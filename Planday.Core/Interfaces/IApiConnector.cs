using Planday.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Planday.Core.Interfaces
{
    public interface IApiConnector
    {

        Task<Root> GetAsync(string searchString, string applicationName);
      
    }
}
