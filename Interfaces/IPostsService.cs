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
        /// Splits string input by ','
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>array of strings</returns>
        public string[] getArrayFromString(string tag);
    }
}
