using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TileMeUpDomain.Models
{
    [Index(nameof(Email), IsUnique = true)]
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
