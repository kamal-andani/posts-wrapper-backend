using zum_rails.DataObjects;
using zum_rails.DTOs;

namespace zum_rails.Interfaces
{
    public interface IThirdPartyClient
    {

        /// <summary>
        /// Fetch posts from third party API for given tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>PostsList</returns>
        public Task<PostsList> FetchPostsByTag(string tag);
    }
}
