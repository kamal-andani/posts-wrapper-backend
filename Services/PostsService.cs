using zum_rails.DTOs;
using zum_rails.Interfaces;

namespace zum_rails.Services
{
    public class PostsService : IPostsService
    {
        public string[] getArrayFromString(string tag)
        {
            //split string by comma and return array of strings
            return tag.Split(',');
            
        }

        public bool TagExistsInQuery(GetPostsQueryDto query)
        {
           if(string.IsNullOrWhiteSpace(query.Tags))
            {
                return false;
            }
           return true;
        }
    }
}
