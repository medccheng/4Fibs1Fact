using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using TileMeUpDomain.Models;

namespace TileMeUpWebApi
{
    public class TileMeUpDbContext : DbContext
    {
        public TileMeUpDbContext(DbContextOptions<TileMeUpDbContext> options)
        : base(options)
        { }
        public DbSet<Accessory> Accessories { get; set; }

        public DbSet<Clothing> Clothings { get; set; }
        public DbSet<Footwear> Footwears { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Jewelry> Jewelries { get; set; }

        public DbSet<Tile> Tiles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Wall> Walls { get; set; }

        public DbSet<WallLayout> WallLayouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }
    }
}
