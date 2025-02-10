namespace NotUyg.Entity
{
    public class Not
    {
        public Not()
        {
            Tags = new List<Tag>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Tarih { get; set; }
        public string acıklama { get; set; }
        public bool Durum {  get; set; }
        public string Baslık {  get; set; }
        public List<Tag> Tags { get; set; }
        public User user { get; set; }



    }
}
