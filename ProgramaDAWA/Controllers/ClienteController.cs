using Microsoft.AspNetCore.Mvc;
using ProgramaDAWA.Repository;
using ProgramaDAWA.Models;

namespace ProgramaDAWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly I_ClienteRepository _clienteRepository;

        public ClienteController(I_ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerClientes()
        {
            var clientes = await _clienteRepository.GetAll();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerClientePorId(int id)
        {
            var cliente = await _clienteRepository.GetById(id);
            if (cliente == null)
            {
                return NotFound("Cliente no Encontrado");
            }
            return Ok(cliente);
        }

        [HttpGet("cedula/{cedula}")]
        public async Task<IActionResult> ObtenerCleintePorCedula(string cedula)
        {
            var cliente = await _clienteRepository.GetByCedula(cedula);
            if (cliente == null)
            {
                return NotFound("Cliente no Encontrado");
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Datos del Cliente Invalidos");
            }
            await _clienteRepository.Create(cliente);
            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest("El Id del cliente no coincide con el Id de la URL.");
            }

            var updatedCliente = await _clienteRepository.Update(cliente);

            if (updatedCliente != null)
            {
                return Ok(updatedCliente);
            }

            return NotFound("Cliente no Encontrado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var cliente = await _clienteRepository.GetById(id);

            if (cliente == null)
            {
                return NotFound("Cliente no Encontrado");
            }

            await _clienteRepository.Delete(cliente.Id);
            return Ok("Cliente Eliminado");
        }
    }
}
