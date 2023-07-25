using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class JewelryRepository : GenericRepository<Jewelry>
    {
        public JewelryRepository(TileMeUpDbContext context) : base(context)
        {
        }
    }
}
