using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TileMeUpDomain.Enums;

namespace TileMeUpDomain.Models
{
    public class Footwear : BaseModel
    {
        [Key]
        public int FootwearId { get; set; }

        public string? Designer { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public Season? Season { get; set; }

        public Gender? Gender { get; set; }

        public Material? Material { get; set; }

        public decimal? Price { get; set; }

        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item? Item { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}