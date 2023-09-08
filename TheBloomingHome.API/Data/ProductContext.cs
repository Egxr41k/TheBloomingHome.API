using Microsoft.EntityFrameworkCore;
using TheBloomingHome.API.Entities;

namespace TheBloomingHome.API.Data;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}
