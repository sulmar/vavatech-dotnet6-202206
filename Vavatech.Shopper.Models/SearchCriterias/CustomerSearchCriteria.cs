using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.Shopper.Models.SearchCriterias
{
    public abstract class SearchCriteria : Base
    {

    }

    public class CustomerSearchCriteria : SearchCriteria
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
