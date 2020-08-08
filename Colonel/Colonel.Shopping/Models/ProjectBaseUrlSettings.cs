using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Shopping.Models
{
    public class ProjectBaseUrlSettings : IProjectBaseUrlSettings
    {
        public string Price { get; set; }
        public string Stock { get; set; }
        public string Product { get; set; }
        public string Shopping { get; set; }
    }
}
