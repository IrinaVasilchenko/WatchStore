using Microsoft.AspNetCore.Mvc;
using WatchesShop.Models;
using WatchesShop.Services;

namespace WatchesShop.Controllers.Api
{
    /// <summary>
    /// REST API для каталогу годинників. Використовує ту саму бізнес-логіку (IWatchService),
    /// що й звичайні MVC-контролери — API це лише інший "вхід" до тих самих даних.
    /// </summary>
    [ApiController]
    [Route("api/watches")]
    [Produces("application/json")]
    public class WatchesApiController : ControllerBase
    {
        private readonly IWatchService _watchService;

        public WatchesApiController(IWatchService watchService)
        {
            _watchService = watchService;
        }

        /// <summary>Отримати список усіх годинників.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Watch>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok(_watchService.GetWatches());
        }

        /// <summary>Отримати годинник за ідентифікатором.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Watch), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var watch = _watchService.GetWatchById(id);
            if (watch == null) return NotFound(new { message = $"Watch with id {id} not found." });
            return Ok(watch);
        }

        /// <summary>Довідник брендів (для випадаючих списків/фільтрів на клієнті).</summary>
        [HttpGet("brands")]
        [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
        public IActionResult GetBrands() => Ok(_watchService.GetBrands());

        /// <summary>Створити новий годинник.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(Watch), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] Watch watch)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _watchService.AddWatch(watch);
            return CreatedAtAction(nameof(GetById), new { id = watch.WatchId }, watch);
        }

        /// <summary>Оновити існуючий годинник.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] Watch watch)
        {
            var existing = _watchService.GetWatchById(id);
            if (existing == null) return NotFound(new { message = $"Watch with id {id} not found." });

            watch.WatchId = id;
            _watchService.UpdateWatch(watch);
            return NoContent();
        }

        /// <summary>Видалити годинник.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var existing = _watchService.GetWatchById(id);
            if (existing == null) return NotFound(new { message = $"Watch with id {id} not found." });

            _watchService.DeleteWatch(id);
            return NoContent();
        }
    }
}
