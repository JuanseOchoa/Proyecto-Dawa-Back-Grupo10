    using Microsoft.AspNetCore.Mvc;
using ProgramaDAWA.Repository;
using ProgramaDAWA.Models;

namespace ProgramaDAWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly I_UsuarioRepository _usuarioRepository;

        public UsuarioController(I_UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Datos del Usuario Invalidos");
            }
            await _usuarioRepository.Create(usuario);
            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest("El Id del usuario no coincide con el Id de la URL.");
            }

            var updatedUsuario = await _usuarioRepository.Update(usuario);

            if (updatedUsuario != null)
            {
                return Ok(updatedUsuario);
            }
            else
            {
                return NotFound("El usuario con el Id especificado no existe.");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuarioToDelete = await _usuarioRepository.GetById(id);
            if (usuarioToDelete == null)
            {
                return NotFound("Usuario no encontrado");
            }
            await _usuarioRepository.Delete(id);
            return NoContent();
        }


    }
}
