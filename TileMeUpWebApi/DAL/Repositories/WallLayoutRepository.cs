using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(TileMeUpDbContext context) : base(context)
        {
        }
    }
}
