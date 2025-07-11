using Microsoft.EntityFrameworkCore;
using ProductsApi.Data;
using ProductsApi.Dto;
using ProductsApi.Services.Contracts;

namespace ProductsApi.Services;

public class ProductService : IProductService
{
    private readonly ProductsDbContext _context;

    public ProductService(ProductsDbContext context) => _context = context;

    public async Task<ICollection<Product>> GetAllProductsAsync() =>
        await _context.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(int id) =>
           await _context.Products.FindAsync(id);

    public async Task<Product> CreateProductAsync(string name, string description)
    {
        Product p = new()
        {
            Name = name,
            Description = description
        };

        await _context.Products.AddAsync(p);
        await _context.SaveChangesAsync();

        return p;
    }

    public async Task EditProductAsync(Product product)
    {
        Product p = (await GetByIdAsync(product.Id))!;

        p.Name = product.Name;
        p.Description = product.Description;

        await _context.SaveChangesAsync();
    }

    public async Task PatchProductAsync(int id, PatchProductDto product)
    {
        Product p = (await GetByIdAsync(id))!;

        p.Name = string.IsNullOrEmpty(product.Name) ? p.Name : product.Name;
        p.Description = string.IsNullOrEmpty(product.Description) ? p.Description : product.Description;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        Product product = (await _context.Products.FindAsync(id))!;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}