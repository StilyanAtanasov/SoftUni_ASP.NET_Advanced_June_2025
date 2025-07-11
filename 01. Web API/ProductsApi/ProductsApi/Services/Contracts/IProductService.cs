using ProductsApi.Data;
using ProductsApi.Dto;

namespace ProductsApi.Services.Contracts;

public interface IProductService
{
    public Task<ICollection<Product>> GetAllProductsAsync();

    public Task<Product?> GetByIdAsync(int id);

    public Task<Product> CreateProductAsync(string name, string description);

    public Task EditProductAsync(Product product);

    public Task PatchProductAsync(int id, PatchProductDto product);

    public Task DeleteProductAsync(int id);
}