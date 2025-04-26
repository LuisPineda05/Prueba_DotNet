using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Domain.Services;
using PruebaTecnica.Pedidos.Resources;
using PruebaTecnica.Shared.Extensions;

namespace PruebaTecnica.Pedidos.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly IMapper _mapper;

        public ProductoController(IProductoService productoService, IMapper mapper)
        {
            _productoService = productoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductoResource>> GetAllAsync()
        {
            var productos = await _productoService.ListAsync();
            return _mapper.Map<IEnumerable<Producto>, IEnumerable<ProductoResource>>(productos);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveProductoResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var producto = _mapper.Map<SaveProductoResource, Producto>(resource);
            var result = await _productoService.SaveAsync(producto);

            if (!result.Success)
                return BadRequest(result.Message);

            var productoResource = _mapper.Map<Producto, ProductoResource>(result.Resource);
            return Ok(productoResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProductoResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var producto = _mapper.Map<SaveProductoResource, Producto>(resource);
            var result = await _productoService.UpdateAsync(id, producto);

            if (!result.Success)
                return BadRequest(result.Message);

            var productoResource = _mapper.Map<Producto, ProductoResource>(result.Resource);
            return Ok(productoResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productoService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var productoResource = _mapper.Map<Producto, ProductoResource>(result.Resource);
            return Ok(productoResource);
        }
    }

}
