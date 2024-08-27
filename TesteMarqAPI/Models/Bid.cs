namespace Models;

public class Bid
{
    public int BidId { get; set; }
    public int UserId {  get; set; }
    public int CarId { get; set; }
    public double BidValue { get; set; }
    public DateTime BidTime { get; set; }
    public Bid() { }
}
