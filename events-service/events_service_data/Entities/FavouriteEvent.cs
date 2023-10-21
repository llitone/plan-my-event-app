using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace events_service_data.Entities;

[Table("favourites_events")]
public class FavouriteEvent
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Column("user_id")]
    public int UserId { get; set; }
    [Column("event_id")]
    public int EventId { get; set; }

    public Event Event { get; set; } = null!;
}
