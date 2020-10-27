using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using FlickrAPI.Core.Interfaces;
using FlickrAPI.Core.Model;
using FlickrAPI.Core.Services;

namespace FlickrAPI.Core.Unit_Of_Work
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceLogger _logger;
        private readonly IHttpClientFactory _clientFactory;
        public IGenericApiConnector<Root> _root;
        private readonly IConfiguration _configuration;
        public string _url;
        public string _path;

        public UnitOfWork(IServiceLogger serviceLogger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _logger = serviceLogger;
            _clientFactory = clientFactory;
            _configuration = configuration;
            _url = _configuration.GetSection("ApiCallSettings").GetSection("url").Value;
            _path = _configuration.GetSection("LogSettings").GetSection("path").Value;
        }

        /// <summary>
        /// Unit of work to return Generic API of type root
        /// </summary>
        public IGenericApiConnector<Root> SearchRootData
        {
            get 
            {
                return _root ?? (_root = new GenericApiConnector<Root>(_logger,_clientFactory,_configuration,_url, _path));
            }
        }
    }
}
