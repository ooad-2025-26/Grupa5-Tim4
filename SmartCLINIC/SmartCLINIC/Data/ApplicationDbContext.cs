using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Models;

namespace SmartClinic.Data
{
    public class ApplicationDbContext : IdentityDbContext<Korisnik>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Termin> Termini { get; set; }

        public DbSet<UslugaKlinike> UslugeKlinike { get; set; }

        public DbSet<Raspored> Rasporedi { get; set; }

        public DbSet<QRKod> QRKodovi { get; set; }

        public DbSet<SistemZaSkeniranjeQRKoda> SistemiZaSkeniranje { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Termin>().ToTable("Termin");
            modelBuilder.Entity<UslugaKlinike>().ToTable("UslugaKlinike");
            modelBuilder.Entity<Raspored>().ToTable("Raspored");
            modelBuilder.Entity<QRKod>().ToTable("QRKod");
            modelBuilder.Entity<SistemZaSkeniranjeQRKoda>().ToTable("SistemZaSkeniranjeQRKoda");
            modelBuilder.Entity<Korisnik>(b =>
            {
                b.Property(u => u.Ime);
                b.Property(u => u.Prezime);
                b.Property(u => u.Uloga);
            });

            modelBuilder.Entity<Termin>()
    .HasOne(t => t.Pacijent)
    .WithMany(k => k.PacijentTermini)
    .HasForeignKey(t => t.PacijentId)
    .OnDelete(DeleteBehavior.Restrict);

     modelBuilder.Entity<Termin>()
                .HasOne(t => t.Doktor)
                .WithMany(k => k.DoktorTermini)
                .HasForeignKey(t => t.DoktorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}