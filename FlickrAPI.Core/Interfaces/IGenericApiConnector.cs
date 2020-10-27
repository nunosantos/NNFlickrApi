using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlickrAPI.Core.Interfaces
{
    public interface IGenericApiConnector<T> where T : class
    {
        Task<T> GetAsync(string searchString, string applicationName);
    }
}
