using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace files_service_data.Entities;

[Table("images")]
public class Image
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Required, Column("path")]
    public string Path { get; set; } = null!;
    [Required, Column("event_id")]
    public int EventId { get; set; }
}
