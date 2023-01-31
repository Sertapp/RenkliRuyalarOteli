using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenkliRuyalarOteli.Entities.Entities.Concrete;

namespace RenkliRuyalarOteli.DAL.EntityConfiguration
{
    public class RoleConfiguration : BaseEntityConfiguration<KullaniciRole>
    {
        public override void Configure(EntityTypeBuilder<KullaniciRole> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.RoleName).IsRequired().HasMaxLength(20);
            builder.HasIndex(p => p.RoleName).IsUnique();

            //builder.HasMany(p => p.Kullanicilar).WithMany(p => p.Roller);
        }
    }
}
