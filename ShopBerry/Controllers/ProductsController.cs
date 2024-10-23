using ShopBerry.Data;
using ShopBerry.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopBerry.Models.Dtos;

[Route("api/[controller]")]
[ApiController]

public class ProductsController : ControllerBase // Используем ControllerBase для API
{
    private readonly ShopContext _context;
    public ProductsController(ShopContext context)
    {
        _context = context;
    }

    // Получение всех продуктов
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.Include(p => p.Shop).ToListAsync();
        return Ok(products); // Возвращаем JSON
    }

    // Получение продукта по ID
    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var product = await _context.Products.Include(p => p.Shop).FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product); // Возвращаем JSON
    }

    // Добавление продукта
    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
        if (ModelState.IsValid)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Color = productDto.Color,
                ShopId = productDto.ShopId
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Details), new { id = product.Id }, product);
        }

        return BadRequest(ModelState);
    }

    // Редактирование продукта
    [HttpPut("edit/{id}")]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return NoContent(); // Возвращаем успешный ответ без содержимого
        }

        return BadRequest(ModelState);
    }

    // Удаление продукта
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent(); // Успешное удаление
    }
}

