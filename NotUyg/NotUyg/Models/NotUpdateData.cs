using NotUyg.Entity;

namespace NotUyg.Models
{
    public class NotUpdateData
    {
        public int Id { get; set; }

        public string acıklama { get; set; }

        public string Baslık { get; set; }

        public List<int> Tags { get; set; }

        public bool Durum { get; set; }
    }
}
