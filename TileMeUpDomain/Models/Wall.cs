using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMeUpDomain.Models
{
    public class Wall : BaseModel
    {
        [Key]
        public int WallId { get; set; }

        public string WallName { get; set; }

        public string WallDescription { get; set;}    



        public int WallLayoutId { get; set; }

        public WallLayout? WallLayout { get; set; }
    }
}
