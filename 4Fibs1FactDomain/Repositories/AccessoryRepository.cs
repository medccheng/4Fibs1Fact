﻿using TileMeUpDomain.Models;

namespace TileMeUpWebApi.DAL.Repositories
{
    public class AccessoryRepository : GenericRepository<Accessory>
    {
        public AccessoryRepository(FibsFactDbContext context) : base(context)
        {
        }
    }
}
