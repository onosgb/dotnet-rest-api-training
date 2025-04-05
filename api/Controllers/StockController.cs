using api.Data;
using api.Dtos.Stock;
using api.interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _IstockRepo;
        public StockController(ApplicationDBContext context, IStockRepository iStockRepo)
        {
            _context = context;
            _IstockRepo = iStockRepo;

        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _IstockRepo.GetAllAsync();
            var stockDto = stocks.Select(stock => stock.ToStockDto());
            return Ok(stockDto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _IstockRepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stock = await _IstockRepo.AddAsync(stockDto);
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatStockRequestDto stockDto)
        {

            var stock = await _IstockRepo.UpdateAsync(id, stockDto);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
            {
                return NotFound();
            }

            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}