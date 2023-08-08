using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class FootwearRepository : GenericRepository<Footwear>
    {
        public FootwearRepository(FibsFactDbContext context) : base(context)
        {
        }
    }
}
