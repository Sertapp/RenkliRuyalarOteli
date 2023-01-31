namespace RenkliRuyalarOteli.Entities.Entities.Concrete
{
    public class Role
    {
        public Role()
        {
            Kullanicilar = new HashSet<KullaniciRole>();
        }
        public string? RoleName { get; set; }

        public ICollection<KullaniciRole> Kullanicilar { get; set; }

    }
}
