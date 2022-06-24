namespace Vavatech.Shopper.Models;

public class Customer : BaseEntity
{
    public string? FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }

    public string Pesel { get; set; }
    public bool IsRemoved { get; set; }

    public decimal Salary { get; set; }
  //  public Order? LastOrder { get; set; }

    public string HashedPassword { get; set; }
    public string ConfirmPassword { get; set; }

    public Employee Owner { get; set; }


}

public class Employee : BaseEntity
{
    public string Username { get; set; }
}

public enum Gender
{
    Male,
    Female
}
