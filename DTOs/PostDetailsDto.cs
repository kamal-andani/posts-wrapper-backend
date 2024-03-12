/* Post Details DTO for our API's response */
namespace zum_rails.DTOs
{
    public class PostDetailsDto
    {
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public int Id { get; set; }
        public int Likes { get; set; }
        public double Popularity { get; set; }
        public uint Reads { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
