using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;

namespace Vavatech.Shopper.Domain.Validators
{
    // Install-Package FluentValidation
    public class CustomerValidator : AbstractValidator<Customer>
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerValidator(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;

            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.Email).EmailAddress().NotEmpty().Must(ExistsEmail);
            RuleFor(p => p.Pesel).Length(11);
            RuleFor(p => p.Password).Equal(p => p.ConfirmPassword);
            RuleFor(p => p.Salary).InclusiveBetween(1, 1000);
            RuleFor(p => p.Pesel).Must(pesel => IsValidPesel(pesel));            
        }

        private bool ExistsEmail(string email)
        {
            return customerRepository.GetByEmail(email) != null;
        }

        private bool IsValidPesel(string pesel)
        {
            return true;
        }
    }
}
