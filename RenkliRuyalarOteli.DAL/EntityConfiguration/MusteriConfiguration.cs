using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenkliRuyalarOteli.Entities.Entities.Concrete;

namespace RenkliRuyalarOteli.DAL.EntityConfiguration
{
    public class MusteriConfiguration : BaseEntityConfiguration<Musteri>
    {
        public override void Configure(EntityTypeBuilder<Musteri> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.MusteriTcNo).HasMaxLength(11);
            builder.Property(p => p.Ad).HasMaxLength(30);
            builder.Property(p => p.Soyad).HasMaxLength(30);
            builder.Property(p => p.CepNo).HasMaxLength(20);

            builder.HasOne(p => p.Kullanici)
                .WithMany(p => p.Musteriler)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
