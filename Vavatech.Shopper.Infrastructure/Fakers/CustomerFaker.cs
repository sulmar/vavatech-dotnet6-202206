using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;

namespace Vavatech.Shopper.Infrastructure.Fakers
{
    // dotnet add package Bogus
    // PM> Install-Package Bogus

    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker(IEmployeeRepository employeeRepository)
        {
            var employees = employeeRepository.Get();

            UseSeed(1);
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Email, (f, customer) => $"{customer.FirstName}.{customer.LastName}@domain.com");
            RuleFor(p => p.Gender, f => (Gender)f.Person.Gender);
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));

            Ignore(p => p.Pesel);
            //Ignore(p => p.LastOrder);
            Ignore(p => p.Salary);
            RuleFor(p => p.HashedPassword, f => "12345");
            Ignore(p => p.ConfirmPassword);

            RuleFor(p => p.Owner, f => new Employee {  Username = "Micheal92" });
        }
    }

    public class EmployeeFaker : Faker<Employee>
    {
        public EmployeeFaker()
        {
            UseSeed(1);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Username, f => f.Person.UserName);
        }
    }
}
