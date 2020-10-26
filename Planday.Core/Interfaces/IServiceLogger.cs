using System;
using System.Collections.Generic;
using System.Text;

namespace Planday.Core.Interfaces
{
    public interface IServiceLogger
    {
        void LogToFile(string data, string path);
    }
}
