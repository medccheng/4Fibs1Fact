using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("WallLayoutId")]

        public WallLayout? WallLayout { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
