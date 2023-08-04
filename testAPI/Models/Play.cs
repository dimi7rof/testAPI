namespace testAPI.Models
{
    public class Play
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Rating { get; set; }
        public int Genre { get; set; }
        public string Description { get; set; }
        public string Screenwriter { get; set;}
    }
}
