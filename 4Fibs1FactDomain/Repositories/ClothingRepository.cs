using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class ClothingRepository : GenericRepository<Clothing>
    {
        public ClothingRepository(FibsFactDbContext context) : base(context)
        {
        }
    }
}
