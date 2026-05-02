using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Models;

namespace SmartClinic.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Termin> Termini { get; set; }
        public DbSet<UslugaKlinike> UslugeKlinike { get; set; }
        public DbSet<Raspored> Rasporedi { get; set; }
        public DbSet<QRKod> QRKodovi { get; set; }
        public DbSet<SistemZaSkeniranjeQRKoda> SistemiZaSkeniranje { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
            modelBuilder.Entity<Termin>().ToTable("Termin");
            modelBuilder.Entity<UslugaKlinike>().ToTable("UslugaKlinike");
            modelBuilder.Entity<Raspored>().ToTable("Raspored");
            modelBuilder.Entity<QRKod>().ToTable("QRKod");
            modelBuilder.Entity<SistemZaSkeniranjeQRKoda>().ToTable("SistemZaSkeniranjeQRKoda");
        }
    }
}