using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Domain;

namespace PruebaTecnica.Infrastructure;
//conexion con sqlite 
public class AppDbContext : DbContext
{
    //representacion de las tablas 
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Currency> Currencies { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    //definicion de la relacion entre tablas y codigos unicos de cada tabla 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Currency>()
            .HasIndex(c => c.Code)
            .IsUnique();

        // relacion User - Addresses (1:N)
        modelBuilder.Entity<Address>()
            .HasOne(a => a.User) // address tiene un user 
            .WithMany(u => u.Addresses) //  un user varios addreses 
            .HasForeignKey(a => a.UserId);  //foreing key
    }
}