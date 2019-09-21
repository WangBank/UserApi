using DnsClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Resilience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Users.Identity.Dtos;

namespace Users.Identity.Services
{
    public class UserService : IUserService
    {
        private string userServiceUrl;
        private IHttpClient httpClient;
        private readonly ILogger<ResilienceHttpClient> _logger;
        public UserService(IHttpClient _httpClient, IOptions<ServiceDisvoveryOptions> serviceOptions, IDnsQuery dnsQuery, ILogger<ResilienceHttpClient> logger)
        {
            httpClient = _httpClient;
            var address = dnsQuery.ResolveService("service.consul", serviceOptions.Value.UserServiceName);
            var addressList = address.First().AddressList;
            var host = addressList.Any() ? addressList.First().ToString() : address.First().HostName;
            var port = address.First().Port;
            _logger = logger;
            userServiceUrl = $"http://{host}:{port}";
        }
        public async Task<int> CheckOrCreate(string phone)
        {
            Phone phoneIn = new Phone { phone = phone };
            //string json = JsonConvert.SerializeObject(phoneIn);
            //var content = new StringContent(json);
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            HttpResponseMessage response;
            string result;
            try
            {
                response = await httpClient.PostAsync(userServiceUrl + "/api/Users/check-or-create", phoneIn); 
                result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("重试之后失败,"  + ex.Message + ex.StackTrace);
                throw;
            }
            
            return int.Parse(result);
        }
    }

    public class Phone
    {
        public  string phone { get; set; }
    }
}
