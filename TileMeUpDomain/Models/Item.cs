using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TileMeUpDomain.Models
{    public class Item : BaseModel
    {
        [Key]
        public int ItemId { get; set; }

        public DateTime? PurchasedOn { get; set; }

        public DateTime? LastUsedOn { get; set; }

        public string? LastUsedAt { get; set; }

        public string? Comment { get; set; }

        public string? ImageUrl { get; set; }


        public int ClosetId { get; set; }

        [ForeignKey("ClosetId")]

        public Closet? Closet { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}