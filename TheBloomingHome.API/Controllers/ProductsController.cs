using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBloomingHome.API.Data;
using TheBloomingHome.API.Entities;

namespace TheBloomingHome.API.Controllers;

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
    public async Task<IActionResult> GetProducts()
    {
        return Ok(await _context.Products.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct([FromRoute] int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> PostProduct([FromBody] Product product)
    {
        _context.Products.Add(product);

        try
        {
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct([FromBody] Product product)
    {
        var existingProduct = await _context.Products.FindAsync(product.Id);
        if (existingProduct == null) return NotFound();

        existingProduct.Name = product.Name;
        existingProduct.ImageSrc = product.ImageSrc;
        existingProduct.Description = product.Description;
        existingProduct.Count = product.Count;
        existingProduct.NewPrice = product.NewPrice;
        existingProduct.OldPrice = product.OldPrice;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        _context.Products.Remove(product);

        try
        {
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}
