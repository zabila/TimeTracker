using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class ClockworkTask
{
    [Key]
    [Column("TaskId")] 
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "ClockworkTaskId is required")]
    public int ClockworkTaskId { get; set; }
    
    public string ClockworkTaskKey { get; set; } = null!;
    
    public DateTime StartedDateTime { get; set; }
    
    public TimeSpan TimeSpentSeconds { get; set; }
    
    [Required (ErrorMessage = "AccountId is required")]
    [ForeignKey(nameof(Account))]
    public Guid AccountId { get; set; }
    public Account? Account { get; set; }
}