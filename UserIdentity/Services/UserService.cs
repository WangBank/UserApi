using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Users.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly string userServiceUrl = "http://localhost:5001/";
        private HttpClient httpClient;

        public UserService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<int> CheckOrCreate(string phone)
        {
            var parms = new Dictionary<string, string> { { "phone", phone } };
            var content = new FormUrlEncodedContent(parms);
            var response = await httpClient.GetStringAsync(userServiceUrl + "api/Users/check-or-create" + $"?phone={phone}");
           
               

            return int.Parse(response);
        }
    }
}
