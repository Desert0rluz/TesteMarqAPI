using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public class TesteMarqAPIContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Bid> Bids { get; set; }

    private string connectionString = "Data Source=KLEBER;Initial Catalog=TesteMarqAPI;Integrated Security=True;" +
        "Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(connectionString);
    }
}