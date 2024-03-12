using zum_rails.DataObjects;
using zum_rails.DTOs;

namespace zum_rails.Interfaces
{
    public interface IPostsService
    {
        /// <summary>
        /// Check if "tag" parameter is present in request query
        /// </summary>
        /// <param name="query"></param>
        /// <returns>true if tag parameter is present, false otherwise</returns>
        public bool TagExistsInQuery(GetPostsQueryDto query);

        /// <summary>
        /// Fetch posts from third party api by tags, combines them without any duplicate posts, 
        /// sorts by specified field (Id as default) in specified order (asc as default)
        /// </summary>
        /// <param name="tags"></param>
        /// <returns>PostsResponseDto</returns>
        public Task<PostsResponseDto> GetFormattedPosts(GetPostsQueryDto query);

        public bool IsSortByParameterValid(string sortby);

        public bool IsDirectionParameterValid(string direction);
    }
}
