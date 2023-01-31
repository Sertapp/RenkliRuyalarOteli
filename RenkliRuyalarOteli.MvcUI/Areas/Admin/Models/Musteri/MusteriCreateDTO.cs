using System.ComponentModel.DataAnnotations;

namespace RenkliRuyalarOteli.MvcUI.Areas.Admin.Models.Musteri
{
    public class MusteriCreateDTO
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "TcNo Zorunlu Alandir")]
        [MaxLength(11)]
        public string MusteriTcNo { get; set; }

        public string Ad { get; set; }
        public string Soyad { get; set; }
        public bool Cinsiyet { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "TcNo Zorunlu Alandir")]
        [MaxLength(11)]
        public string CepNo { get; set; }

        public List<string>? Roller { get; set; }
    }
}
