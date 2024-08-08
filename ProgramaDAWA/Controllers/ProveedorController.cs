using Microsoft.AspNetCore.Mvc;
using ProgramaDAWA.Repository;
using ProgramaDAWA.Models;

namespace ProgramaDAWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        
        private readonly I_ProveedorRepository _proveedorRepository;

        public ProveedorController(I_ProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProveedores()
        {
            var proveedores = await _proveedorRepository.GetAll();
            return Ok(proveedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProveedorPorId(int id)
        {
            var proveedor = await _proveedorRepository.GetById(id);
            if (proveedor == null)
            {
                return NotFound("Proveedor no Encontrado");
            }
            return Ok(proveedor);
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<IActionResult> ObtenerProveedorPorCodigo(string codigo)
        {
            var proveedor = await _proveedorRepository.GetByCode(codigo);
            if (proveedor == null)
            {
                return NotFound("Proveedor no Encontrado");
            }
            return Ok(proveedor);
        }

        [HttpPost]
        public async Task<IActionResult> CrearProveedor([FromBody] Proveedor proveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Datos del Proveedor Invalidos");
            }
            await _proveedorRepository.Create(proveedor);
            return Ok(proveedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProveedor(int id, [FromBody] Proveedor proveedor)
        {
            if (id != proveedor.Id)
            {
                return BadRequest("El Id del proveedor no coincide con el Id de la URL.");
            }

            var updatedProveedor = await _proveedorRepository.Update(proveedor);

            if (updatedProveedor != null)
            {
                return Ok(updatedProveedor);
            }
            return NotFound("El Proveedor no ah sido encontrado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProveedor(int id)
        {
            var proveedor = await _proveedorRepository.GetById(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            await _proveedorRepository.Delete(proveedor.Id);
            return Ok();
        }

    }
}
