using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Web
{
    public class RequestLog 
    {
        public Guid Id { get; set; }
        public string PathAndQueryString { get; set; }
        public string LoginName { get; set; }
        public string Browser { get; set; }
        public DateTime VisitDate { get; set; }
        public string IpAddress { get; set; }
    }
}
