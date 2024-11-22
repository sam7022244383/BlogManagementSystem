using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employe : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set ; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get ; set; }

        public string? Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

    }
}
