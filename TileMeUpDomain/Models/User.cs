using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMeUpDomain.Models
{
    public class User : BaseModel
    {
        [Key]
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Nickname { get; set; }
        public string Email { get; set; }
        
    }
}
