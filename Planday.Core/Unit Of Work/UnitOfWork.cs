using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Planday.Core.Interfaces;
using Planday.Core.Model;
using Planday.Core.Services;

namespace Planday.Core.Unit_Of_Work
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceLogger _logger;
        private readonly IHttpClientFactory _clientFactory;
        public IGenericApiConnector<Root> _root;
        private readonly IConfiguration _configuration;

        public UnitOfWork(IServiceLogger serviceLogger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _logger = serviceLogger;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public IGenericApiConnector<Root> SearchRootData
        {
            get 
            {
                return _root ?? (_root = new GenericApiConnector<Root>(_logger,_clientFactory,_configuration));
            }
        }
    }
}
