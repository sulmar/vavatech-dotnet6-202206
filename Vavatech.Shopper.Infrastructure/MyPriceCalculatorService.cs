using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.Shopper.Domain.Services;

namespace Vavatech.Shopper.Infrastructure
{
    public class MyPriceCalculatorService : IPriceCalculatorService
    {
        public decimal CalculatePrice(int productId, int customerId)
        {
            return 100m;
        }
    }
}
