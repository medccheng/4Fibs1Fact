using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(FibsFactDbContext context) : base(context)
        {
        }
    }
}
