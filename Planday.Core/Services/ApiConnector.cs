using Newtonsoft.Json;
using Planday.Core.Interfaces;
using Planday.Core.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Planday.Core.Services
{
    public class ApiConnector : IApiConnector
    {
        private readonly IServiceLogger _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public ApiConnector(IServiceLogger logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<Root> GetAsync(string searchString, string applicationName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    var url = _configuration.GetSection("ApiCallSettings").GetSection("url").Value;
                    url = url.Replace("{searchString}", searchString);
                    using (var client = _clientFactory.CreateClient(applicationName))
                    {
                        var result = await client.GetStringAsync(url);
                        result = result.Replace("jsonFlickrApi", "").Replace("(", "").Replace(")", "");


                        Root value = JsonConvert.DeserializeObject<Root>(result);
                        _logger.LogToFile(result);
                        return value;
                    }
                }
                else
                {
                    throw new ArgumentNullException();
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogToFile(ex.Message);
                return null; ;
            }


        }
    }
}
