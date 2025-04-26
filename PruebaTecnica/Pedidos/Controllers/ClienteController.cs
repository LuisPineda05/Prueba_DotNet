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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ClienteResource>> GetAllAsync()
        {
            var clientes = await _clienteService.ListAsync();
            return _mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteResource>>(clientes);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveClienteResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var cliente = _mapper.Map<SaveClienteResource, Cliente>(resource);
            var result = await _clienteService.SaveAsync(cliente);

            if (!result.Success)
                return BadRequest(result.Message);

            var clienteResource = _mapper.Map<Cliente, ClienteResource>(result.Resource);
            return Ok(clienteResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveClienteResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var cliente = _mapper.Map<SaveClienteResource, Cliente>(resource);
            var result = await _clienteService.UpdateAsync(id, cliente);

            if (!result.Success)
                return BadRequest(result.Message);

            var clienteResource = _mapper.Map<Cliente, ClienteResource>(result.Resource);
            return Ok(clienteResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _clienteService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var clienteResource = _mapper.Map<Cliente, ClienteResource>(result.Resource);
            return Ok(clienteResource);
        }
    }

}
