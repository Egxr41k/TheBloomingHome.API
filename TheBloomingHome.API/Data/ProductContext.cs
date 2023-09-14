using Microsoft.EntityFrameworkCore;
using TheBloomingHome.API.Entities;

namespace TheBloomingHome.API.Data;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductDetails> ProductsDetails { get; set; } = null!;
    public DbSet<Feature> Features { get; set; } = null!;
    public DbSet<Property> Stats { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
}
