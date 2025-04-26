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
    public class PedidoProductoController : ControllerBase
    {
        private readonly IPedidoProductoService _pedidoProductoService;
        private readonly IMapper _mapper;

        public PedidoProductoController(IPedidoProductoService pedidoProductoService, IMapper mapper)
        {
            _pedidoProductoService = pedidoProductoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PedidoProductoResource>> GetAllAsync()
        {
            var items = await _pedidoProductoService.ListAsync();
            return _mapper.Map<IEnumerable<PedidoProducto>, IEnumerable<PedidoProductoResource>>(items);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SavePedidoProductoResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var pedidoProducto = _mapper.Map<SavePedidoProductoResource, PedidoProducto>(resource);
            var result = await _pedidoProductoService.SaveAsync(pedidoProducto);

            if (!result.Success)
                return BadRequest(result.Message);

            var resourceResult = _mapper.Map<PedidoProducto, PedidoProductoResource>(result.Resource);
            return Ok(resourceResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SavePedidoProductoResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var pedidoProducto = _mapper.Map<SavePedidoProductoResource, PedidoProducto>(resource);
            var result = await _pedidoProductoService.UpdateAsync(id, pedidoProducto);

            if (!result.Success)
                return BadRequest(result.Message);

            var resourceResult = _mapper.Map<PedidoProducto, PedidoProductoResource>(result.Resource);
            return Ok(resourceResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _pedidoProductoService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var resourceResult = _mapper.Map<PedidoProducto, PedidoProductoResource>(result.Resource);
            return Ok(resourceResult);
        }
    }

}
