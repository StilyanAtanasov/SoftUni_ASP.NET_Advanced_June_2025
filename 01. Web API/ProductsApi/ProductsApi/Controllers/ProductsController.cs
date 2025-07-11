using Microsoft.AspNetCore.Mvc;
using ProductsApi.Data;
using ProductsApi.Dto;
using ProductsApi.Services.Contracts;

namespace ProductsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService) => _productService = productService;

    /// <summary>
    /// Retrieves a collection of all available products.
    /// </summary>
    /// <remarks>This method fetches all products asynchronously using the underlying product service. The
    /// returned collection may be empty if no products are available.</remarks>
    /// <returns>An <see cref="IActionResult"/> containing a collection of <see cref="Product"/> objects. The response has a
    /// status code of 200 (OK) if the operation is successful.</returns>
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        ICollection<Product> products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <remarks>This method performs an asynchronous operation to fetch the product details.  If the product
    /// is found, it returns an HTTP 200 OK response with the product data.  Otherwise, it returns an HTTP 404 Not Found
    /// response.</remarks>
    /// <param name="id">The unique identifier of the product to retrieve.</param>
    /// <returns>An <see cref="IActionResult"/> containing the product if found, or a <see cref="NotFoundResult"/> if no product
    /// exists with the specified identifier.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        Product? product = await _productService.GetByIdAsync(id);
        if (product is null) return NotFound();

        return Ok(product);
    }

    /// <summary>
    /// Creates a new product with the specified details.
    /// </summary>
    /// <param name="product">The product details to create, including <see cref="Product.Name"/> and <see cref="Product.Description"/>.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see
    /// cref="BadRequestObjectResult"/> if the product data is invalid, or  <see cref="CreatedAtActionResult"/> with the
    /// created product if successful.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name) || string.IsNullOrWhiteSpace(product.Description))
            return BadRequest("Invalid product data.");

        Product p = await _productService.CreateProductAsync(product.Name, product.Description);
        return CreatedAtAction(nameof(CreateProduct), p);
    }

    /// <summary>
    /// Updates an existing product with the specified details.
    /// </summary>
    /// <remarks>The <paramref name="product"/> parameter must include valid values for the <see
    /// cref="Product.Name"/> and <see cref="Product.Description"/> properties.</remarks>
    /// <param name="id">The unique identifier of the product to update.</param>
    /// <param name="product">The updated product details. The <see cref="Product.Id"/> property must match the <paramref name="id"/>
    /// parameter.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation: <list type="bullet">
    /// <item><description><see cref="BadRequestResult"/> if the product data is invalid or the <paramref name="id"/>
    /// does not match the <see cref="Product.Id"/>.</description></item> <item><description><see
    /// cref="NotFoundResult"/> if no product with the specified <paramref name="id"/> exists.</description></item>
    /// <item><description><see cref="NoContentResult"/> if the product was successfully updated.</description></item>
    /// </list></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> EditProduct(int id, [FromBody] Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name) || string.IsNullOrWhiteSpace(product.Description))
            return BadRequest("Invalid product data.");

        Product? existingProduct = await _productService.GetByIdAsync(id);
        if (existingProduct is null) return NotFound();

        if (id != product.Id) return BadRequest("Product ID mismatch.");

        await _productService.EditProductAsync(product);
        return NoContent();
    }

    /// <summary>
    /// Partially updates the product with the specified ID using the provided patch data.
    /// </summary>
    /// <remarks>This method applies a partial update to an existing product. Only the fields specified in the
    /// <paramref name="product"/> parameter will be updated. The product must exist in the system;  otherwise, a 404
    /// Not Found response is returned.</remarks>
    /// <param name="id">The unique identifier of the product to update.</param>
    /// <param name="product">The patch data containing the fields to update and their new values.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="NotFoundResult"/> if
    /// the product with the specified ID does not exist.  Returns <see cref="NoContentResult"/> if the update is
    /// successful.</returns>
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchProduct(int id, [FromBody] PatchProductDto product)
    {
        Product? existingProduct = await _productService.GetByIdAsync(id);
        if (existingProduct is null) return NotFound();

        await _productService.PatchProductAsync(id, product);
        return NoContent();
    }

    /// <summary>
    /// Deletes the product with the specified identifier.
    /// </summary>
    /// <remarks>This method performs a lookup for the product by its identifier. If the product is found, it
    /// is deleted  and the deleted product is returned in the response. If the product is not found, a 404 Not Found
    /// response is returned.</remarks>
    /// <param name="id">The unique identifier of the product to delete.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="NotFoundResult"/> if
    /// the product with the specified identifier does not exist.  Returns <see cref="OkObjectResult"/> containing the
    /// deleted product if the operation is successful.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        Product? p = await _productService.GetByIdAsync(id);
        if (p is null) return NotFound();

        await _productService.DeleteProductAsync(id);
        return Ok(p);
    }
}