using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly TesteMarqAPIContext _context;
    private readonly GetAddressByCep _addressService;

    public UserController(TesteMarqAPIContext context, GetAddressByCep addressService)
    {
        _context = context;
        _addressService = addressService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!string.IsNullOrEmpty(user.UserCEP))
        {
            Address endereco = await _addressService.ObterEnderecoPorCepAsync(user.UserCEP);

            _context.Addresses.Add(endereco);
            await _context.SaveChangesAsync();

            user.AddressID = endereco.AddressID;
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == id);

        if (user == null)
        {
            return NotFound();
        }

        var endereco = await _context.Addresses
            .FirstOrDefaultAsync(e => e.AddressID == user.AddressID);

        var userResponse = new
        {
            user.UserId,
            user.UserName,
            user.UserEmail,
            user.UserCEP,
            user.AddressID,
            Address = endereco
        };

        return Ok(userResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(updatedUser.UserName))
        {
            user.UserName = updatedUser.UserName;
        }
        if (!string.IsNullOrEmpty(updatedUser.UserEmail))
        {
            user.UserEmail = updatedUser.UserEmail;
        }
        if (!string.IsNullOrEmpty(updatedUser.UserCEP))
        {
            try
            {
                Address endereco = await _addressService.ObterEnderecoPorCepAsync(updatedUser.UserCEP);
                if (endereco != null)
                {
                    _context.Addresses.Add(endereco);
                    await _context.SaveChangesAsync();
                    user.AddressID = endereco.AddressID;
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest($"Erro ao localizar o endereço: {ex.Message}");
            }
        }

        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        if (user.AddressID != 0)
        {
            var endereco = await _context.Addresses.FindAsync(user.AddressID);
            if (endereco != null)
            {
                _context.Addresses.Remove(endereco);
            }
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }
}
