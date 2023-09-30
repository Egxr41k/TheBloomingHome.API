using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBloomingHome.API.Data;
using TheBloomingHome.API.Entities;
using System.Linq;

namespace TheBloomingHome.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetails>> GetProductDetails(int id)
        {
            var productDetails = await _context.Products.FindAsync(id);

            if (productDetails == null)
            {
                return NotFound();
            }
            return new ProductDetails()
            {
                Id = id,
                Features = await _context.Features.Where(feature => feature.ProductId == id).ToListAsync(),
                Stats = await _context.Stats.Where(property => property.ProductId == id).ToListAsync(),
            };
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductDetails), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Product product)
        {
            _context.Products.Update(product);
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
