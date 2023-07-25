using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class WallRepository : GenericRepository<Wall>
    {
        public WallRepository(TileMeUpDbContext context) : base(context)
        {
        }
    }
}
