using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace events_service_data.Entities;

[Table("categories")]
public class Category
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Required, Column("name")]
    public string Name { get; set; } = null!;
}
