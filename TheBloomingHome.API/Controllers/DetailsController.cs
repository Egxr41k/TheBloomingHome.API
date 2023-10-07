using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TheBloomingHome.API.Data;
using TheBloomingHome.API.Entities;


namespace TheBloomingHome.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DetailsController : ControllerBase
{
    private readonly ProductContext _context;
    private List<ProductDetails> detailsList;

    public DetailsController(ProductContext context)
    {
        _context = context;
        detailsList = Synchronize().Result;
    }

    private async Task<List<ProductDetails>> Synchronize()
    {
        var result = new List<ProductDetails>();
        var products = await _context.Products.ToListAsync();

        foreach (var product in products)
        {
            var productDetails = new ProductDetails()
            {
                Id = product.Id,
                Features = await _context.Features
                    .Where(feature => feature.ProductId == product.Id)
                    .ToListAsync(),
                Stats = await _context.Stats
                    .Where(property => property.ProductId == product.Id)
                    .ToListAsync(),
            };

            result.Add(productDetails);
        }
        return result;
    }

    [HttpGet]
    public async Task<IActionResult> GetDetailsList()
    {
        return Ok(detailsList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails([FromRoute] int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product != null)
        {
            var productDetails = detailsList.Find(details => details.Id == id);
            if (productDetails == null) return NotFound();
            return Ok(productDetails);
        }
        else return NotFound();

    }

    [HttpPost]
    public async Task<IActionResult> PostDetails([FromBody] ProductDetails details)
    {

        details.Stats.ForEach(property =>
        {
            _context.Stats.Add(property);
        });

        details.Features.ForEach(feature =>
        {
            _context.Features.Add(feature);
        });
        detailsList.Add(details);

        try
        {
            await _context.SaveChangesAsync();
            return Ok(details);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDetails([FromBody] ProductDetails details)
    {
        details.Stats.ForEach(property =>
        {
            var existingProperty = _context.Stats.FirstOrDefault(p => p.Id == property.Id);
            if (existingProperty != null)
            {
                existingProperty.Name = property.Name;
                existingProperty.Value = property.Value;
            }
            else _context.Stats.Add(property);
        });

        details.Features.ForEach(feature =>
        {
            var existingProperty = _context.Features.FirstOrDefault(f => f.Id == feature.Id);
            if (existingProperty != null)
            {
                existingProperty.Title = feature.Title;
                existingProperty.Description = feature.Description;
                existingProperty.ImageSrc = feature.ImageSrc;
            }
            else _context.Features.Add(feature);
        });

        try
        {
            await _context.SaveChangesAsync();
            return Ok(details);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDetails([FromRoute] int id)
    {
        var details = detailsList.Find(details => details.Id == id);
        if (details == null) return NotFound();

        details.Stats.ForEach(property =>
        {
            _context.Stats.Remove(property);
        });

        details.Features.ForEach(feature =>
        {
            _context.Features.Remove(feature);
        });
        detailsList.Remove(details);

        try
        {
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}
