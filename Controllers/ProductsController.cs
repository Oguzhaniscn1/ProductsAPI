using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;
using ProductsAPI.DTO;
using SQLitePCL;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {

        private readonly ProductsContext _context;

        public ProductsController(ProductsContext context)
        {
            _context=context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products= await _context.Products.Where(i=>i.IsActive).Select(p=>ProductToDTO(p)).AsNoTracking().ToListAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {
            if(id is null) 
            {
                return NotFound();//404
            }

            var p= await _context.Products.Select(p=> ProductToDTO(p)).AsNoTracking().FirstOrDefaultAsync(i=>i.ProductId==id);
            
            if(p is null)
            {
                return NotFound();
            }

            return Ok(p);

        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct),new {id=entity.ProductId},entity);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,Product entity)
        {
            if(id!=entity.ProductId)
            {
                return BadRequest();
            }
            var product=await _context.Products.FirstOrDefaultAsync(i=>i.ProductId==id);
            if(product is null)
            {
                return NotFound();
            }

            product.ProducName=entity.ProducName;
            product.Price=entity.Price;
            product.IsActive=entity.IsActive;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return NotFound();
            }

            return NoContent();//204 her şey normal geriye değer döndürmez.

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if(id is null)
            {
                return NotFound();
            }
            var product=await _context.Products.FirstOrDefaultAsync(i=>i.ProductId==id);
            if(product is null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                
                return NotFound();
            }
            return NoContent();

        }



        private static ProductDTO ProductToDTO(Product p)
        {
            return new ProductDTO
            {
                ProductId=p.ProductId,
                ProducName=p.ProducName,
                Price=p.Price

            };
        }

    }

    

}