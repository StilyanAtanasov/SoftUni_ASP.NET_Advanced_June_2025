using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Data;

public sealed class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) => Database.EnsureCreated();

    public DbSet<Product> Products { get; set; } = null!;
}