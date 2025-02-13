using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACar.Models;

namespace RentACar.Data
{
    public class ApplicationDbContext : IdentityDbContext<Account>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vozilo> Vozila { get; set; }
        public DbSet<Account> Racuni { get; set; }
        public DbSet<Dostava> Dostave { get; set; }
        public DbSet<Poslovnica> Poslovnice { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vozilo>().ToTable("Vozila");
            modelBuilder.Entity<Account>().ToTable("Racun");
            modelBuilder.Entity<Dostava>().ToTable("Dostava");
            modelBuilder.Entity<Poslovnica>().ToTable("Poslovnica");
            modelBuilder.Entity<Rezervacija>().ToTable("Rezervacija");

            base.OnModelCreating(modelBuilder);
        }
    }
}
