namespace TODO.WebApi.Models

{
    public class Book
    {
        public int BookId { get; set; }


        public int UserId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }


        public double? Rating { get; set; }
        public string Review { get; set; }
    }
}
