using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class ETicaretContext : DbContext
    {
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<KullaniciDetayi> KullaniciDetaylari { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Windows Authentication
            //string connectionString = "server=.;database=BA_ETicaretCore8523;trusted_connection=true;multipleactiveresultsets=true;";

            //SQL Server Authentication
            string connectionString = "server=.;database=BA_ETicaretCore8523;user id=sa;password=123;multipleactiveresultsets=true;";

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Urun>()
                //.ToTable("ETicaretUrunler") Tablo ismi değiştirir
                .HasOne(urun => urun.Kategori)
                .WithMany(kategori => kategori.Urunler)
                .HasForeignKey(urun => urun.KategoriId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Kullanici>()
                //.ToTable("ETicaretKullanicilar")
                .HasOne(k => k.Rol)
                .WithMany(r => r.Kullanicilar)
                .HasForeignKey(k => k.RolId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetayi>()
               //.ToTable("ETicaretKullaniciDetaylari")
               .HasOne(x => x.Kullanici)
               .WithOne(x => x.KullaniciDetayi)
               .HasForeignKey<KullaniciDetayi>(x => x.KullaniciId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}