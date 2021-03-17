using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOM.IS4Host.Models
{
    public class ApplicationEventStore
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string EventType { get; set; }
        public string Message { get; set; }
        public string ActivityId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ProcessId { get; set; }
        public string LocalIpAddress { get; set; }
        public string RemoteIpAddress { get; set; }
        public string UserName { get; set; }
    }
}
