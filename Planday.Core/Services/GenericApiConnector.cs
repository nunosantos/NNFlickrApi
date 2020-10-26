using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Planday.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Planday.Core.Services
{
    public class GenericApiConnector<T> : IGenericApiConnector<T> where T : class
    {
        private readonly IServiceLogger _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public GenericApiConnector(IServiceLogger logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<T> GetAsync(string searchString,string applicationName)
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
                        result = result.Replace("jsonFlickrApi","").Replace("(", "").Replace(")", "");
                        
                        
                        T value = JsonConvert.DeserializeObject<T>(result);
                        _logger.LogToFile(result);
                        return value;
                    }
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch (Exception ex)
            {
                _logger.LogToFile(ex.Message);
                return null;
            }
            
            
        }
    }
}
