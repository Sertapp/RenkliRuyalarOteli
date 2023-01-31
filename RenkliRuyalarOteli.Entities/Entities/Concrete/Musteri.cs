using RenkliRuyalarOteli.Entities.Entities.Abstract;

namespace RenkliRuyalarOteli.Entities.Entities.Concrete
{
    public class Musteri : BaseEntity
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public bool Cinsiyet { get; set; }
        public string MusteriTcNo { get; set; }
        public string CepNo { get; set; }

        public Guid KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }
        public ICollection<Rezervasyon> Rezervasyonlari { get; set; }
    }
}
