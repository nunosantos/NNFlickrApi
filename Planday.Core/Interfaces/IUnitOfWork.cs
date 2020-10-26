using Planday.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planday.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericApiConnector<Root> SearchRootData { get; }
    }
}
