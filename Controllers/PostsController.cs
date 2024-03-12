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
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        /// <summary>
        /// Fetch posts from third-party endpoint based on specified tags, sorts result based on sortBy parameter with specified direction
        /// Endpoint -> https://localhost:7016/api/posts
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns>PostsResponseDto</returns>
        [HttpGet]
        [ProducesResponseType(200, Type=typeof(PostsResponseDto))]
        [ProducesResponseType(400, Type=typeof(ApiException))]
        public async Task<IActionResult> GetPosts([FromQuery] GetPostsQueryDto queryDto)
        {
            // Checks if tags parameter is not present in the request
            if (!_postsService.TagExistsInQuery(queryDto))
                return BadRequest(new ApiException(400, "tags parameter is required", "Request must include tag as a query parameter"));

            // Validate sortBy parameter if exists
            if (queryDto.SortBy != null && !_postsService.IsSortByParameterValid(queryDto.SortBy.ToLower()))
                return BadRequest(new ApiException(400, "sortBy parameter is invalid", ""));

            // Validate direction parameter if exists
            if (queryDto.Direction != null && !_postsService.IsDirectionParameterValid(queryDto.Direction.ToLower()))
                return BadRequest(new ApiException(400, "direction parameter is invalid", ""));

            // Get list of all posts that have at least one tag specified in tags parameter 
            PostsResponseDto posts = await _postsService.GetFormattedPosts(queryDto);
            return Ok(posts);
        }
    }
}
