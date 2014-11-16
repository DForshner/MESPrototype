using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Web.Concurrency
{
    public interface IConcurrencyAware
    {
        string ConcurrencyVersion { get; set; }
    }
}