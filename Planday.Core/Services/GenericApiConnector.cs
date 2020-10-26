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
        public IConfiguration _configuration;
        public string _url;

        public GenericApiConnector(IServiceLogger logger, IHttpClientFactory clientFactory, IConfiguration configuration, string url)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _configuration = configuration;
            _url = url;
        }

        /// <summary>
        /// Generic Get method which returns a JSON object of type T
        /// </summary>
        /// <param name="searchString">The string to be used in the query string</param>
        /// <param name="applicationName">the name of the http client instance</param>
        /// <returns></returns>
        public async Task<T> GetAsync(string searchString,string applicationName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    
                    _url = _url.Replace("{searchString}", searchString);
                    using (var client = _clientFactory.CreateClient(applicationName))
                    {
                        var response = await client.GetStringAsync(_url);
                        response = response.Replace("jsonFlickrApi","").Replace("(", "").Replace(")", "");
                        
                        T objectValue = JsonConvert.DeserializeObject<T>(response);
                        _logger.LogToFile(response);
                        return objectValue;
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
