using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Account
{
    [Key]
    [Column("AccountId")] 
    public Guid Id { get; set; }
    
    public string? ClockworkAccountId { get; set; }
    
    [Required(ErrorMessage = "UserName is required")]
    public string? UserName { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
    
    [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters")]
    public string? FirstName { get; set; }
    
    [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters")]
    public string? LastName { get; set; }
    
    public int Type { get; set; }

    [MaxLength(100, ErrorMessage = "Maximum length for the AuthorizationToken is 100 characters")]
    public string? AuthorizationToken { get; set; } = null!;
    
    public ICollection<Account>? Tasks { get; set; }
}