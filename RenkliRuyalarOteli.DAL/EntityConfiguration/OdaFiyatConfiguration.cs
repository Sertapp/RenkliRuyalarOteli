using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenkliRuyalarOteli.Entities.Entities.Abstract;
using RenkliRuyalarOteli.Entities.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenkliRuyalarOteli.DAL.EntityConfiguration
{
    public class OdaFiyatConfiguration:BaseEntityConfiguration<OdaFiyat>
    {
        public override void Configure(EntityTypeBuilder<OdaFiyat> builder)
        {
            base.Configure(builder);
            builder.HasOne(p => p.Kullanici)
              .WithMany(p => p.OdaFiyatlari)
              .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

        }
    }
}
