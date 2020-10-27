using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlickrAPI.Core.Model
{
    public class Root
    {
        public Photos photos { get; set; }
        public string stat { get; set; }
    }
}
