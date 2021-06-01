using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DemoApp.AppEntities
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ProductType { get; set; }

    }
}
