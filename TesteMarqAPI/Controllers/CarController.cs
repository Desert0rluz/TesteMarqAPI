using Microsoft.AspNetCore.Mvc;
using Models;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly TesteMarqAPIContext _context;

    public CarController(TesteMarqAPIContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCar([FromBody] Car car)
    {
        if (car == null)
        {
            return BadRequest("O carro não pode ser nulo");
        }

        _context.Cars.Add(car);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCar), new { id = car.CarId }, car);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCar(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null)
        {
            return NotFound();
        }

        return Ok(car);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCar(int id, [FromBody] Car updatedCar)
    {

        var car = await _context.Cars.FindAsync(id);
        if (car == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(updatedCar.CarBrand))
        {
            car.CarBrand = updatedCar.CarBrand;
        }
        if (!string.IsNullOrEmpty(updatedCar.CarModel))
        {
            car.CarModel = updatedCar.CarModel;
        }
        if (updatedCar.CarYear.HasValue)
        {
            car.CarYear = updatedCar.CarYear;
        }
        if (updatedCar.CarPrice.HasValue)
        {
            car.CarPrice = updatedCar.CarPrice;
        }

        _context.Entry(car).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null)
        {
            return NotFound();
        }

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("cars")]
    public async Task<IActionResult> GetCars()
    {
        var cars = await _context.Cars.ToListAsync();
        return Ok(cars);
    }

}
