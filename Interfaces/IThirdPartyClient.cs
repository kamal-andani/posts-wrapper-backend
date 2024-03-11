using zum_rails.DataObjects;
using zum_rails.DTOs;

namespace zum_rails.Interfaces
{
    public interface IThirdPartyClient
    {

        /// <summary>
        /// Fetch posts from third party API for all Tags
        /// </summary>
        /// <param name="tags"></param>
        /// <returns>PostsList</returns>
        public Task<PostsList> FetchPostsByAllTags(string[] tags);
    }
}
