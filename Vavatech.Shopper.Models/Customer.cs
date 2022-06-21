using System.ComponentModel.DataAnnotations;

namespace Vavatech.Shopper.Models;

public class Customer : BaseEntity
{
    public string? FirstName { get; set; }
    public string LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; }
    public Gender Gender { get; set; }

    [Required]
    [StringLength(11, MinimumLength = 11)]    
    public string Pesel { get; set; }
    public bool IsRemoved { get; set; }

    [Range(1, 1000)]
    public decimal Salary { get; set; }
    public Order LastOrder { get; set; }

    [Compare("ConfirmPassword")]
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public enum Gender
{
    Male,
    Female
}
