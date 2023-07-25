using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMeUpDomain.Models
{
    public class Tile : BaseModel
    {
        public int TileId { get; set; }

        public int TilePosition { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }



        public int WallId { get; set; }
        public Wall Wall { get; set; }

        public int ItemId { get; set; }
        public Item? Item { get; set; }
    }
}
