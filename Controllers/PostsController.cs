using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using zum_rails.DataObjects;
using zum_rails.DTOs;
using zum_rails.Errors;
using zum_rails.Interfaces;

namespace zum_rails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IThirdPartyClient _thirdPartyClient;
        private readonly IPostsService _postsService;

        public PostsController(IThirdPartyClient thirdPartyClient, IPostsService postsService)
        {
            _thirdPartyClient = thirdPartyClient;
            _postsService = postsService;
        }

        /// <summary>
        /// Fetch posts from third-party endpoint based on specified tags, sorts result based on sortBy parameter with specified direction
        /// Endpoint -> https://localhost:7016/api/posts
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns>PostList</returns>
        [HttpGet]
        [ProducesResponseType(200, Type=typeof(PostsList))]
        [ProducesResponseType(400, Type=typeof(ApiException))]
        public async Task<IActionResult> GetPosts([FromQuery] GetPostsQueryDto queryDto)
        {
            // Checks if tags parameter is not present in the request
            if (!_postsService.TagExistsInQuery(queryDto))
                return BadRequest(new ApiException(400, "tags parameter is required", "Request must include tag as a query parameter"));

            // get array of tags from comma seperated tag values
            string[] tags = _postsService.getArrayFromString(queryDto.Tags);

            // Get list of all posts that have at least one tag specified in tags parameter 
            PostsList posts = await _thirdPartyClient.FetchPostsByAllTags(tags);
            return Ok(posts);
           
           
            

        }
    }
}
