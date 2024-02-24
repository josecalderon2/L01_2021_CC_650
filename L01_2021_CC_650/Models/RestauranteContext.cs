using L01_2021_CC_650.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace L01_2021_CC_650.Models
{
    public class RestauranteContext : DbContext
    {

        public RestauranteContext(DbContextOptions<RestauranteContext> options) : base(options)
        {

        }
        public DbSet<Pedidos>Pedidos { get; set; }

        public DbSet<Platos> Platos { get; set; }

        public DbSet<Motoristas> Motoristas { get; set; }



    }
}
