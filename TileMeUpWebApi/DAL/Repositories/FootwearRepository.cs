using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class FootwearRepository : GenericRepository<Footwear>
    {
        public FootwearRepository(TileMeUpDbContext context) : base(context)
        {
        }
    }
}
