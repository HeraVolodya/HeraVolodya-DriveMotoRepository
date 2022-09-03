using DriveMoto.Models;
using Microsoft.EntityFrameworkCore;

namespace DriveMoto.DataBase
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
    }

    //зробити наступним чином
    //public class APIDbContext : DbContext
    //{
    //    public APIDbContext(DbContextOptions options) : base(options)
    //    {

    //    }
    //    public DbSet<Client> Clients { get; set; }
    //    public DbSet<Product> Products { get; set; }
    //}
}
