using RenkliRuyalarOteli.Entities.Entities.Abstract;

namespace RenkliRuyalarOteli.Entities.Entities.Concrete
{
    public class KullaniciRole : BaseEntity
    {
        public Guid KullaniciId { get; set; }
        public Guid RoleId { get; set; }

        public Kullanici Kullanici { get; set; }
        public Role Role { get; set; }
    }
}
