using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class ItemRepository : GenericRepository<Item>
    {
        public ItemRepository(TileMeUpDbContext context) : base(context)
        {
        }
    }
}
