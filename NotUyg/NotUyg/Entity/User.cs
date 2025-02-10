namespace NotUyg.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Soyad { get; set; }
        public List<Not> Nots { get; set; }
    }
}
