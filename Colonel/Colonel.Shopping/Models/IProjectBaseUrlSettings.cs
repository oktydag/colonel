using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Shopping.Models
{
    public interface IProjectBaseUrlSettings
    {
        string Price { get; set; }
        string Stock { get; set; }
        string Product { get; set; }
        string Shopping { get; set; }
        string User { get; set; }

    }
}
