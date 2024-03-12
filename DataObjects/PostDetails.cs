/* Post Details Object for third party API */
namespace zum_rails.DataObjects
{
    public class PostDetails
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
