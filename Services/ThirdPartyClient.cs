using Newtonsoft.Json;
using zum_rails.DataObjects;
using zum_rails.Interfaces;

namespace zum_rails.Services
{
    public class ThirdPartyClient: IThirdPartyClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ThirdPartyClient> _logger;
        private string dataRepositoryBaseAPI;

        public ThirdPartyClient(IHttpClientFactory httpClientFactory, ILogger<ThirdPartyClient> logger)
        {
            dataRepositoryBaseAPI = "https://api.hatchways.io/";
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        //Get list of posts for all specified tags
        public async Task<PostsList> FetchPostsByAllTags(string[] tag)
        {
            // TODO::
            return await FetchPostsByTag(tag[0]);
        }

        // Get list of posts by specific tag
        private async Task<PostsList> FetchPostsByTag(String tag)
        {

            //construct endpoint for required tag
            string getPostsEndpoint = dataRepositoryBaseAPI + "assessment/blog/posts?tag=" + tag;

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, getPostsEndpoint);


            //create client to perform http requests
            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            // Check if request returned 2xx status code
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // Serialize Http response as string
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                //Deserialize string as object of PostsList
                var responseAsObj = JsonConvert.DeserializeObject<PostsList>(responseString);
                return responseAsObj;
            }
            else
            {
                _logger.LogError($"Error retrieving posts from thirdparty {httpResponseMessage}");
                throw new BadHttpRequestException($"Error from thrid party api: {httpResponseMessage.StatusCode}. Please check logs for more details");
            }
      
        }
    }
}
