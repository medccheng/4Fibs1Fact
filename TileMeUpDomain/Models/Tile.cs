using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMeUpDomain.Models
{
    public class Tile : BaseModel
    {
        [Key]
        public int TileId { get; set; }

        public int TilePosition { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }



        public int WallId { get; set; }

        [ForeignKey("WallId")]
        public Wall Wall { get; set; }

        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item? Item { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
