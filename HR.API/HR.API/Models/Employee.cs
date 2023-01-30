using System.ComponentModel.DataAnnotations;

namespace HR.API.Models;

public class Employee
{
    public int Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string FullName { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Department { get; set; }  
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
