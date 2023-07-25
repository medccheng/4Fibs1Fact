using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class ClothingRepository : GenericRepository<Clothing>
    {
        public ClothingRepository(TileMeUpDbContext context) : base(context)
        {
        }
    }
}
