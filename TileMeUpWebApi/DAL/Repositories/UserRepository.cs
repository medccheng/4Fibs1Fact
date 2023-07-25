using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class WallLayoutRepository : GenericRepository<WallLayout>
    {
        public WallLayoutRepository(TileMeUpDbContext context) : base(context)
        {
        }
    }
}
