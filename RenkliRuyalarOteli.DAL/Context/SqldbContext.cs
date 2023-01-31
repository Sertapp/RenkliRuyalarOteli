using Microsoft.EntityFrameworkCore;
using RenkliRuyalarOteli.Entities.Entities.Abstract;
using RenkliRuyalarOteli.Entities.Entities.Concrete;
using System.Reflection;

namespace RenkliRuyalarOteli.DAL.Context
{
    public class SqldbContext : DbContext
    {
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Oda> Odalar { get; set; }
        public DbSet<OdaFiyat> OdaFiyatlari { get; set; }
        public DbSet<Rezervasyon> Rezervasyonlar { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<RezervasyonDetay> RezervasyonDetaylari { get; set; }
        public DbSet<KullaniciRole> Roller { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=RenkliRuyalarOteli;Trusted_Connection=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSucces,CancellationToken cancellationToken=default)
        {
            UpdateSoftDeleteStatus();
            return base.SaveChangesAsync(acceptAllChangesOnSucces,cancellationToken);
        }
        private void UpdateSoftDeleteStatus()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["Status"] = Status.Active;
                        entry.CurrentValues["CreateDate"] = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.CurrentValues["Status"] = Status.Update;
                        entry.CurrentValues["UpdateDate"] = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["Status"] = Status.Delete;
                        entry.CurrentValues["UpdateDate"] = DateTime.Now;
                        break;



                }
            }
        }
    }
}
