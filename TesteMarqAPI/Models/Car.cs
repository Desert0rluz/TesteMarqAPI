namespace Models;

public class Car
{
    public int CarId { get; set; }
    public string? CarBrand { get; set; }
    public string? CarModel { get; set; }
    public int? CarYear { get; set; }
    public double? CarPrice { get; set; }

    public Car() { }
}
