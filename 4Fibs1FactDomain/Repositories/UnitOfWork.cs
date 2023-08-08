using TileMeUpDomain.Models;
using TileMeUpWebApi.DAL.Repositories;

namespace TileMeUpWebApi.DAL
{

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private FibsFactDbContext context;
        private GenericRepository<Accessory> accessoryRepository;
        private GenericRepository<Closet> closetRepository;
        private GenericRepository<Clothing> clothingRepository;
        private GenericRepository<Footwear> footwearRepository;
        private GenericRepository<Item> itemRepository;
        private GenericRepository<Jewelry> jewelryRepository;
        private GenericRepository<Tile> tileRepository;
        private GenericRepository<User> userRepository;
        private GenericRepository<Wall> wallRepository;
        private GenericRepository<WallLayout> wallLayoutRepository;


        public UnitOfWork(FibsFactDbContext dbContext)
        {
            context = dbContext;
        }
        public GenericRepository<Accessory> AccessoryRepository
        {
            get
            {
                if (accessoryRepository == null)
                {
                    accessoryRepository = new GenericRepository<Accessory>(context);
                }
                return accessoryRepository;
            }
        }

        public GenericRepository<Closet> ClosetRepository
        {
            get
            {
                if (closetRepository == null)
                {
                    closetRepository = new GenericRepository<Closet>(context);
                }
                return closetRepository;
            }
        }

        public GenericRepository<Clothing> ClothingRepository
        {
            get
            {

                if (clothingRepository == null)
                {
                    clothingRepository = new GenericRepository<Clothing>(context);
                }
                return clothingRepository;
            }
        }


        public GenericRepository<Footwear> FootwearRepository
        {
            get
            {

                if (footwearRepository == null)
                {
                    footwearRepository = new GenericRepository<Footwear>(context);
                }
                return footwearRepository;
            }
        }

        public GenericRepository<Item> ItemRepository
        {
            get
            {

                if (itemRepository == null)
                {
                    itemRepository = new GenericRepository<Item>(context);
                }
                return itemRepository;
            }
        }

        public GenericRepository<Jewelry> JewelryRepository
        {
            get
            {

                if (jewelryRepository == null)
                {
                    jewelryRepository = new GenericRepository<Jewelry>(context);
                }
                return jewelryRepository;
            }
        }


        public GenericRepository<Tile> TileRepository
        {
            get
            {

                if (tileRepository == null)
                {
                    tileRepository = new GenericRepository<Tile>(context);
                }
                return tileRepository;
            }
        }


        public GenericRepository<User> UserRepository
        {
            get
            {

                if (userRepository == null)
                {
                    userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public GenericRepository<WallLayout> WallLayoutRepository
        {
            get
            {

                if (wallLayoutRepository == null)
                {
                    wallLayoutRepository = new GenericRepository<WallLayout>(context);
                }
                return wallLayoutRepository;
            }
        }

        public GenericRepository<Wall> WallRepository
        {
            get
            {

                if (wallRepository == null)
                {
                    wallRepository = new GenericRepository<Wall>(context);
                }
                return wallRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
