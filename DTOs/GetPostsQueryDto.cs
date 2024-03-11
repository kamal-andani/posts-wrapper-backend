/* Query Parameters for fetching posts  */
using zum_rails.Constants;

namespace zum_rails.DTOs
{
    public class GetPostsQueryDto
    {
        public string Tags { get; set; }
        public SortBy SortBy { get; set; }
        public SortDirection Direction { get; set; }
    }
}
