using RenkliRuyalarOteli.Entities.Entities.Abstract;

namespace RenkliRuyalarOteli.Entities.Entities.Concrete
{
    public class Rezervasyon : BaseEntity
    {
        //Hangi odaya rezervasyon yapildi
        public Guid OdaId { get; set; }
        public Oda Oda { get; set; }

        //Odanin o tarihteki fiyati nedir
        public Guid OdaFiyatId { get; set; }
        public OdaFiyat OdaFiyat { get; set; }

        //Odaya giris ve cikis tarihleri
        public DateTime GirisTarihi { get; set; }
        public DateTime CikisTarihi { get; set; }

        public ICollection<RezervasyonDetay> RezervasyonDetaylari { get; set; }

        public Guid KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }

    }
}
