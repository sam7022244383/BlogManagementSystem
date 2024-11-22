using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class EmployeRespoance
    {
        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag? ETag { get; set; }
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
