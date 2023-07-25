using TileMeUpDomain.Models;
using TileMeUpWebApi.DAL.Repositories;

namespace TileMeUpWebApi.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<Accessory> AccessoryRepository { get; }
        GenericRepository<Closet> ClosetRepository { get; }
        GenericRepository<Clothing> ClothingRepository { get; }
        GenericRepository<Footwear> FootwearRepository { get; }
        GenericRepository<Item> ItemRepository { get; }
        GenericRepository<Jewelry> JewelryRepository { get; }
        GenericRepository<Tile> TileRepository { get; }
        GenericRepository<User> UserRepository { get; }
        GenericRepository<Wall> WallRepository { get; }
        GenericRepository<WallLayout> WallLayoutRepository { get; }

        void Save();

        void Dispose();
    }
}