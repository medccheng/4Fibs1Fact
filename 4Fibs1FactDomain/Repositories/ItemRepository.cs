using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class ItemRepository : GenericRepository<Item>
    {
        public ItemRepository(FibsFactDbContext context) : base(context)
        {
        }
    }
}
