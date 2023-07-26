using System.ComponentModel.DataAnnotations;

namespace TileMeUpDomain.Models
{
    public class WallLayout : BaseModel
    {
        [Key]
        public int WallLayoutId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Schema { get; set; }
    }
}