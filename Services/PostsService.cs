using AutoMapper;
using zum_rails.Constants;
using zum_rails.DataObjects;
using zum_rails.DTOs;
using zum_rails.Interfaces;

namespace zum_rails.Services
{
    public class PostsService : IPostsService
    {
        private readonly IThirdPartyClient _thirdPartyClient;
        private readonly IMapper _mapper;

        public PostsService(IThirdPartyClient thirdPartyClient, IMapper mapper)
        {
            _thirdPartyClient = thirdPartyClient;
            _mapper = mapper;
        }

        public bool TagExistsInQuery(GetPostsQueryDto query)
        {
            if (string.IsNullOrWhiteSpace(query.Tags))
            {
                return false;
            }
            return true;
        }

        public bool IsSortByParameterValid(string sortby)
        {
            return Enum.IsDefined(typeof(SortBy), sortby);
        }

        public bool IsDirectionParameterValid(string direction)
        {
            return Enum.IsDefined(typeof(SortDirection), direction);
        }

        public async Task<PostsResponseDto> GetFormattedPosts(GetPostsQueryDto queryDto)
        {
            // get array of tags from comma seperated tag values
            string[] tags = GetArrayFromString(queryDto.Tags);


            // Get required unique posts
            PostsList uniqueCombinedPosts = await FetchAllPostsHavingAtLeastOneSpecifiedTag(tags);

            
            // sort the result
            var sortedPostsThirdParty = SortPosts(uniqueCombinedPosts.Posts, queryDto.SortBy, queryDto.Direction);

            //convert result in required DTO format
            var sortedPostResult = _mapper.Map<IEnumerable<PostDetailsDto>>(sortedPostsThirdParty);

            return new PostsResponseDto { Posts = sortedPostResult};
        }


        /// <summary>
        /// Get posts for each tag in the parameter and combine the result to have unique posts
        /// </summary>
        /// <param name="tags"></param>
        /// <returns>PostsList</returns>
        private async Task<PostsList> FetchAllPostsHavingAtLeastOneSpecifiedTag(string[] tags)
        {
            List<PostDetails> postsDetailList = new List<PostDetails>();

            foreach (var tag in tags)
            {
                //get posts for a tag
                var postsForTag = await _thirdPartyClient.FetchPostsByTag(tag);
                
                // Union set of retrieved postdetails lists based on postdetails.Id as Key
                postsDetailList = postsDetailList.UnionBy(postsForTag.Posts, x => x.Id).ToList();
            }
            return new PostsList{ Posts = postsDetailList};

        }

        /// <summary>
        /// Splits string input by ','
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>array of strings</returns>
        private string[] GetArrayFromString(string tag)
        {
            //split string by comma and return array of strings
            return tag.Split(',');
            
        }

        /// <summary>
        /// Sort Posts by specified field in specified direction. 
        /// Default sorting field is id. Default sorting direction is asc
        /// </summary>
        /// <param name="posts"></param>
        /// <param name="sortByField"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        private IEnumerable<PostDetails> SortPosts(IEnumerable<PostDetails> posts, string sortByField, string sortDirection)
        {
            IEnumerable<PostDetails> sortedPosts = null;
            sortDirection = sortDirection?.ToLower();
            sortByField = sortByField?.ToLower();
            switch (sortByField)
            {
                case "reads":
                    sortedPosts = sortDirection == "desc" ? posts.OrderByDescending(p => p.Reads) : posts.OrderBy(p => p.Reads);
                    break;
                case "likes":
                    sortedPosts = sortDirection == "desc" ? posts.OrderByDescending(p => p.Likes) : posts.OrderBy(p => p.Likes);
                    break;
                case "popularity":
                    sortedPosts = sortDirection == "desc" ? posts.OrderByDescending(p => p.Popularity) : posts.OrderBy(p => p.Popularity);
                    break;
                default:
                    sortedPosts = sortDirection == "desc" ? posts.OrderByDescending(p => p.Id) : posts.OrderBy(p => p.Id);
                    break;
            }
            return sortedPosts;

        }
    }
}
