using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class TileRepository : GenericRepository<Tile>
    {
        public TileRepository(FibsFactDbContext context) : base(context)
        {
        }
    }
}
