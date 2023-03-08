using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;
using Entities.LinkModels;

namespace Entities.Models;

public class ClockworkTask {
    [Key] [Column("TaskId")] public Guid Id { get; set; }

    [Required(ErrorMessage = "ClockworkTaskId is required")]
    public int ClockworkTaskId { get; set; }

    public string ClockworkTaskKey { get; set; } = null!;

    public DateTime StartedDateTime { get; set; }

    public TimeSpan TimeSpentSeconds { get; set; }

    [Required(ErrorMessage = "AccountId is required")]
    [ForeignKey(nameof(Account))]
    public Guid AccountId { get; set; }

    private void WriteLinksToXml(string key, object value, XmlWriter writer) {
        writer.WriteStartElement(key);
        if (value.GetType() == typeof(List<Link>)) {
            foreach (var val in (value as List<Link>)!) {
                writer.WriteStartElement(nameof(Link));
                WriteLinksToXml(nameof(val.Href), val.Href!, writer);
                WriteLinksToXml(nameof(val.Rel), val.Rel!, writer);
                WriteLinksToXml(nameof(val.Method), val.Method!, writer);
                writer.WriteEndElement();
            }
        } else {
            writer.WriteString(value.ToString());
        }
        writer.WriteEndElement();
    }
}