using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.Shopper.Models
{
    public class Order : BaseEntity
    {
        public DateOnly DateOrder { get; set; }
        public Customer Customer { get; set; }
    }
}
