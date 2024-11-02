using Microsoft.EntityFrameworkCore;
using Prova.MarQ.Domain.Entities;

namespace Prova.MarQ.Infra;

public class ProvaMarqDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<RegistroPonto> RegistroPontos { get; set; }

    public ProvaMarqDbContext(DbContextOptions<ProvaMarqDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>()
            .HasMany(c => c.Employees)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Name)
            .HasMaxLength(100);

        modelBuilder.Entity<Employee>()
            .Property(e => e.PIN)
            .HasMaxLength(4)
            .IsRequired();

        modelBuilder.Entity<Company>()
            .HasQueryFilter(c => !c.IsDeleted);

        modelBuilder.Entity<Employee>()
            .HasQueryFilter(e => !e.IsDeleted);

        modelBuilder.Entity<RegistroPonto>()
            .Property(rp => rp.Ponto)
            .IsRequired();

        modelBuilder.Entity<RegistroPonto>()
            .HasOne(rp => rp.Employee)
            .WithMany(e => e.RegistroPontos)
            .HasForeignKey(rp => rp.IdEmployee)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<RegistroPonto>()
            .HasOne(rp => rp.Company)
            .WithMany(c => c.RegistroPontos)
            .HasForeignKey(rp => rp.IdCompany)
            .OnDelete(DeleteBehavior.NoAction);

        base.OnModelCreating(modelBuilder);
    }
}
