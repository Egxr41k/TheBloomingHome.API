using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBloomingHome.API.Data;
using TheBloomingHome.API.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheBloomingHome.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly ProductContext _context;

        public DetailsController(ProductContext context)
        {
            _context = context;
        }

        // GET api/<DetailsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetails>> Get(int id)
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

        // POST api/<DetailsController>
        [HttpPost]
        public async void Post([FromBody] ProductDetails details)
        {
            details.Stats.ForEach(property =>
            {
                _context.Stats.Add(property);
            });

            details.Features.ForEach(feature =>
            {
                _context.Features.Add(feature);
            });

            await _context.SaveChangesAsync();
        }

        // PUT api/<DetailsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDetails details)
        {
            try
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

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при обновлении деталей продукта");
            }
        }

        // DELETE api/<DetailsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
