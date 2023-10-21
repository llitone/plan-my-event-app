using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace events_service_data.Entities;

[Table("events")]
public class Event
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Column("creator_id")]
    public int CreatorId { get; set; }
    [Column("category_id")]
    public int CategoryId { get; set; }
    [Required, Column("name")]
    public string Name { get; set; } = null!;
    [Column("description")]
    public string? Description { get; set; }
    [Required, Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Required, Column("start_at")]
    public DateTime StartAt { get; set; }
    [Column("address")]
    public string? Address { get; set; }
    [Column("price")]
    public decimal? Price { get; set; }
    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    public Category Category { get; set; } = null!;

    public ICollection<EventEntry> EventEntries { get; set; } = null!;
    public ICollection<FavouriteEvent> FavouriteEvents { get; set; } = null!;
}
