using System.ComponentModel.DataAnnotations;
using TileMeUpDomain.Enums;

namespace TileMeUpDomain.Models
{
    public class Clothing : BaseModel
    {
        [Key]
        public int ClothingId { get; set; }

        public string? Designer { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public Season? Season { get; set; }

        public Gender? Gender { get; set; }

        public Material? Material { get; set; }

        public decimal? Price { get; set; }




        public int ItemId { get; set; }
        public Item? Item { get; set; }
    }
}