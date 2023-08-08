using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class ClosetRepository : GenericRepository<Item>
    {
        public ClosetRepository(FibsFactDbContext context) : base(context)
        {
        }
    }
}
