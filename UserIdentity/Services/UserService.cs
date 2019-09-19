using DnsClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
        private HttpClient httpClient;

        public UserService(HttpClient _httpClient, IOptions<ServiceDisvoveryOptions> serviceOptions, IDnsQuery dnsQuery)
        {
            httpClient = _httpClient;
            var address = dnsQuery.ResolveService("service.consul", serviceOptions.Value.UserServiceName);
            var addressList = address.First().AddressList;
            var host = addressList.Any() ? addressList.First().ToString() : address.First().HostName;
            var port = address.First().Port;
            userServiceUrl = $"http://{host}:{port}";
        }
        public async Task<int> CheckOrCreate(string phone)
        {
            Phone phoneIn = new Phone { phone = phone };
            string json = JsonConvert.SerializeObject(phoneIn);
            var content = new StringContent(json);
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            HttpResponseMessage response;
            string result;
            //创建HttpClient（注意传入HttpClientHandler）
            using (var http = new HttpClient(handler))
            {
                response = await httpClient.PostAsync(userServiceUrl + "/api/Users/check-or-create", content); //await异步等待回应
                //await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）
                result = await response.Content.ReadAsStringAsync();//result就是返回的结果。
            }

            return int.Parse(result);
        }
    }

    public class Phone
    {
        public  string phone { get; set; }
    }
}
