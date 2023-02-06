using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Account
{
    [Column("AccountId")] public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public int Type { get; set; }

    [MaxLength(100, ErrorMessage = "Maximum length for the Token is 100 characters")]
    public string? Token { get; set; } = null!;
}