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
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IMapper _mapper;

        public PedidoController(IPedidoService pedidoService, IMapper mapper)
        {
            _pedidoService = pedidoService;
            _mapper = mapper;
        }

        // GET: api/v1/Pedido
        [HttpGet]
        public async Task<IEnumerable<PedidoResource>> GetAllAsync()
        {
            var pedidos = await _pedidoService.ListAsync();
            return _mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoResource>>(pedidos);
        }

        // GET: api/v1/Pedido/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _pedidoService.GetByIdAsync(id);
            if (!response.Success)
                return NotFound(response.Message);

            var pedidoResource = _mapper.Map<Pedido, PedidoResource>(response.Resource);
            return Ok(pedidoResource);
        }

        // POST: api/v1/Pedido
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] SavePedidoResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var pedido = _mapper.Map<SavePedidoResource, Pedido>(resource);
            var response = await _pedidoService.SaveAsync(pedido, pedido.Productos.ToList());

            if (!response.Success)
                return BadRequest(response.Message);

            var pedidoResource = _mapper.Map<Pedido, PedidoResource>(response.Resource);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Resource.Id }, pedidoResource);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateAsync(int id, [FromBody] SavePedidoResource resource)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.GetErrorMessages());

        //    var pedido = _mapper.Map<SavePedidoResource, Pedido>(resource);
        //    var response = await _pedidoService.UpdateAsync(id, pedido);

        //    if (!response.Success)
        //        return BadRequest(response.Message);

        //    var pedidoResource = _mapper.Map<Pedido, PedidoResource>(response.Resource);
        //    return Ok(pedidoResource);
        //}

        // DELETE: api/v1/Pedido/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _pedidoService.DeleteAsync(id);
            if (!response.Success)
                return NotFound(response.Message);

            return NoContent();
        }
    }
}


