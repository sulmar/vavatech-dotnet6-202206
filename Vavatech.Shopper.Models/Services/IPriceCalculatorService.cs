using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.Shopper.Domain.Services
{
    public interface IPriceCalculatorService
    {
        decimal CalculatePrice(int productId, int customerId);
    }
}
