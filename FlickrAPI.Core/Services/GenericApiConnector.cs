﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using FlickrAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlickrAPI.Core.Services
{
    public class GenericApiConnector<T> : IGenericApiConnector<T> where T : class
    {
        private readonly IServiceLogger _logger;
        private readonly IHttpClientFactory _clientFactory;
        public IConfiguration _configuration;
        public string _url;
        public string _path;

        public GenericApiConnector(IServiceLogger logger, IHttpClientFactory clientFactory, IConfiguration configuration, string url, string path)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _configuration = configuration;
            _url = url;
            _path = path;
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
                        _logger.LogToFile(response, _path);
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
                _logger.LogToFile(ex.Message, _path);
                return null;
            }
            
            
        }
    }
}
