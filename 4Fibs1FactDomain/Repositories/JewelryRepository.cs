using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class JewelryRepository : GenericRepository<Jewelry>
    {
        public JewelryRepository(FibsFactDbContext context) : base(context)
        {
        }
    }
}
