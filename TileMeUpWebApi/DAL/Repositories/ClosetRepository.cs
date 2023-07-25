using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class ClosetRepository : GenericRepository<Item>
    {
        public ClosetRepository(TileMeUpDbContext context) : base(context)
        {
        }
    }
}
