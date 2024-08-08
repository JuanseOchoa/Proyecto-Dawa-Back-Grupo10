using Microsoft.AspNetCore.Mvc;
using ProgramaDAWA.Repository;
using ProgramaDAWA.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgramaDAWA.Context;

namespace ProgramaDAWA.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class CategoriaController : Controller
        {
            private readonly I_CategoriaRepository _categoriaRepository;
            private readonly AppDbContext _context; // Agrega esta línea para el contexto


        public CategoriaController(I_CategoriaRepository categoriaRepository, AppDbContext context)
            {
                _categoriaRepository = categoriaRepository;
               _context = context;


        }

        [HttpGet]
            public async Task<IActionResult> ObtenerCategorias()
            {
                var categorias = await _categoriaRepository.GetAll();
                return Ok(categorias);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> ObtenerCategoriaPorId(int id)
            {
                var categoria = await _categoriaRepository.GetById(id);
                if (categoria == null)
                {
                    return NotFound("Categoría no Encontrada");
                }
                return Ok(categoria);
            }

        [HttpGet("categoria/{id}/producto")]
        public async Task<IActionResult> ObtenerProductosPorIdCategoria(int id)
        {
            var categoria = await _categoriaRepository.GetById(id);
            if (categoria == null)
            {
                return NotFound("Categoría no Encontrada");
            }

            var productos = _context.Productos.Where(p => p.Categoria.Id == id).ToList();
            return Ok(productos);
        }


        [HttpPost]
            public async Task<IActionResult> CrearCategoria([FromBody] Categoria categoria)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Datos de la Categoría Inválidos");
                }
                await _categoriaRepository.Create(categoria);
                return Ok(categoria);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> ActualizarCategoria(int id, [FromBody] Categoria categoria)
            {
                if (id != categoria.Id)
                {
                    return BadRequest("El Id de la categoría no coincide con el Id de la URL.");
                }

                var updatedCategoria = await _categoriaRepository.Update(categoria);

                if (updatedCategoria != null)
                {
                    return Ok(updatedCategoria);
                }

                return NotFound("Categoría no Encontrada");
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> EliminarCategoria(int id)
            {
                var categoria = await _categoriaRepository.GetById(id);

                if (categoria == null)
                {
                    return NotFound("Categoría no Encontrada");
                }

                await _categoriaRepository.Delete(categoria.Id);
                return Ok("Categoría Eliminada");
            }
        }

    }
