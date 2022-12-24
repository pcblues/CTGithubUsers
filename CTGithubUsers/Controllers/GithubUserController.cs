using CTGithubUsers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace CTGithubUsers.Controllers
{
    [ApiController]
    [Route("retrieveUsers")]
    public class GithubUserController : ControllerBase
    {

        

        private readonly ILogger<GithubUserController> _logger;

        public GithubUserController(ILogger<GithubUserController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<GithubUser> Get([FromQuery] List<string> username)
        {
            List<GithubUser> result = new List<GithubUser>();

            // Remove duplicates
            List<string> noDupes = username.Distinct().ToList();


            // Use GraphQL to get users
            result = GetGithubUsers(noDupes);

            
            return result;

        }


        [NonAction]
        public  List<GithubUser> GetGithubUsers (List<string> userNames)
        {
            List<GithubUser> result = new List<GithubUser>();


            using (HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://api.github.com/users/") })
            {
                // Needed for request to work
                httpClient.DefaultRequestHeaders.Add("User-Agent", "ThisApp");

                // iterates in order?
                foreach (string userName in userNames)
                {
                    try
                    {
                        var myRequest = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress + userName);

                        using (var myResponse = httpClient.Send(myRequest))
                        {
                            dynamic responseObj;

                            myResponse.EnsureSuccessStatusCode();
                            var response = myResponse.Content.ReadAsStringAsync().Result;

                            responseObj = JsonConvert.DeserializeObject<dynamic>(response);
                            
                            GithubUser user = new GithubUser
                            {
                                Name = responseObj.name,
                                Company = responseObj.company,
                                NumberFollowers = responseObj.followers,
                                NumberRepositories = responseObj.public_repos,
                                Login = responseObj.login
                            };
                            result.Add(user);



                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }


            }


            // sort by name
            return result.OrderBy(u => u.Name).ToList() ;



        }


    }
}
