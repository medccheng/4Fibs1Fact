using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMeUpDomain.Models
{
    public class Closet : BaseModel
    {
        [Key]
        public int ClosetId { get; set; }

        public string ClosetName { get; set; }  

        public string ClosetDescription { get; set; }

    }
}
