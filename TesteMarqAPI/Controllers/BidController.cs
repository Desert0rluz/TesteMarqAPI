using Microsoft.AspNetCore.Mvc;
using Models;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class BidController : ControllerBase
{
    private readonly TesteMarqAPIContext _context;

    public BidController(TesteMarqAPIContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBid([FromBody] Bid bid)
    {
        var car = await _context.Cars.FindAsync(bid.CarId);
        if (car == null)
        {
            return BadRequest("Carro não localizado.");
        }

        var user = await _context.Users.FindAsync(bid.UserId);
        if (user == null)
        {
            return BadRequest("Usuario não localizado.");
        }

        if (bid.BidValue < car.CarPrice)
        {
            return BadRequest("Valor do lance não pode ser menor que o valor minimo do carro");
        }

        bid.BidTime = DateTime.UtcNow;

        _context.Bids.Add(bid);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBid), new { id = bid.BidId }, bid);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBid(int id)
    {
        var bid = await _context.Bids
            .FirstOrDefaultAsync(b => b.BidId == id);

        if (bid == null)
        {
            return NotFound();
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == bid.UserId);

        var car = await _context.Cars
            .FirstOrDefaultAsync(c => c.CarId == bid.CarId);

        var bidResponse = new
        {
            bid.BidId,
            bid.BidValue,
            bid.BidTime,
            User = user,
            Car = car
        };

        return Ok(bidResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBid(int id, [FromBody] Bid updatedBid)
    {
        var bid = await _context.Bids.FindAsync(id);
        if (bid == null)
        {
            return NotFound();
        }

        if (updatedBid.CarId > 0)
        {
            var car = await _context.Cars.FindAsync(updatedBid.CarId);
            if (car == null)
            {
                return BadRequest("Carro não localizado.");
            }

            if (updatedBid.BidValue < car.CarPrice)
            {
                return BadRequest("Valor do lance não pode ser menor que o valor minimo do carro.");
            }

            bid.CarId = updatedBid.CarId;
        }

        if (updatedBid.UserId > 0)
        {
            var user = await _context.Users.FindAsync(updatedBid.UserId);
            if (user == null)
            {
                return BadRequest("Usuário não localizado.");
            }

            bid.UserId = updatedBid.UserId;
        }

        if (updatedBid.BidValue > 0)
        {
            bid.BidValue = updatedBid.BidValue;
        }

        _context.Entry(bid).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBid(int id)
    {
        var bid = await _context.Bids.FindAsync(id);
        if (bid == null)
        {
            return NotFound();
        }

        _context.Bids.Remove(bid);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetBids()
    {
        var bids = await _context.Bids
            .Select(b => new
            {
                b.BidId,
                b.UserId,
                b.CarId,
                b.BidValue,
                b.BidTime,
                User = _context.Users.FirstOrDefault(u => u.UserId == b.UserId),
                Car = _context.Cars.FirstOrDefault(c => c.CarId == b.CarId)
            })
            .ToListAsync();

        return Ok(bids);
    }
}
