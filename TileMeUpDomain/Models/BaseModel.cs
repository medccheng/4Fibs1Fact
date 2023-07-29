using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMeUpDomain.Models
{
    public class BaseModel
    {
        public string Status { get; set; }

        public string Access { get; set; }

        public DateTime CreatedOn { get; set; }        

        public DateTime? UpdatedOn { get; set; }



        public int? CreatedById { get; set; }

        public string? CreatedBy { get; set; }

        public int? UpdateById { get; set; }
        
        public string? UpdatedBy { get; set; }

    }
}
