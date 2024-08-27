namespace Models;

public class User
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
    public string? UserCEP { get; set; }
    public int AddressID { get; set; }

    public User() { }
}
