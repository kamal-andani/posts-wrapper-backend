/* PostsList DTO for our API's response */
namespace zum_rails.DTOs
{
    public class PostsResponseDto
    {
        public IEnumerable<PostDetailsDto> Posts { get; set; }
    }
}
