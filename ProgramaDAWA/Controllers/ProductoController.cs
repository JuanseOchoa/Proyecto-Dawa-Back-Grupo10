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
    public class ProductoController : ControllerBase
    {
        private readonly I_ProductoRepository _productoRepository;
        private readonly AppDbContext _context; // Agrega esta línea para el contexto


        public ProductoController(I_ProductoRepository productoRepository, AppDbContext context)
        {
            _productoRepository = productoRepository;
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductos()
        {
            var productos = await _productoRepository.GetAll();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProductoPorId(int id)
        {
            var producto = await _productoRepository.GetById(id);
            if (producto == null)
            {
                return NotFound("Producto no Encontrado");
            }
            return Ok(producto);
        }
        [HttpGet("Nombre/{Nombre}")]
        public async Task<IActionResult> ObtenerProductoPorNombre(string Nombre)
        {
            var producto = await _productoRepository.GetByNombre(Nombre );
            if (producto == null)
            {
                return NotFound("Producto no Encontrado");
            }
            return Ok(producto);
        }
        [HttpGet("categoria/Nombre{Nombre}")]
        public async Task<IActionResult> ObtenerProductoNombre(string Nombre)
        {
            var producto = await _productoRepository.GetByNombre(Nombre);

            if (producto == null)
            {
                return NotFound("Producto no Encontrado");
            }

            return Ok(producto);
        }
        [HttpGet("categoria/Nombre{nombreCategoria}")]
        public async Task<IActionResult> ObtenerProductosPorNombreCategoria(string nombreCategoria)
        {
            var productos = await _productoRepository.GetByCategoriaNombre(nombreCategoria);

            if (productos == null || !productos.Any())
            {
                return NotFound("No se encontraron productos para la categoría especificada");
            }

            return Ok(productos);
        }
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Datos del Producto Inválidos");
            }
            await _productoRepository.Create(producto);
            return Ok(producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest("El Id del producto no coincide con el Id de la URL.");
            }

            var updatedProducto = await _productoRepository.Update(producto);

            if (updatedProducto != null)
            {
                return Ok(updatedProducto);
            }

            return NotFound("Producto no Encontrado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _productoRepository.GetById(id);

            if (producto == null)
            { 

                return NotFound("Producto no Encontrado");
            }

            await _productoRepository.Delete(producto.Id);
            return Ok("Producto Eliminado");
        }

        [HttpGet("categoria/{idcategoria}")]
        public async Task<IActionResult> ObtenerProductosPorIdCategoria(int idcategoria)
        {
            var productos = await _context.Productos
                .Where(p => p.Categoria.Id == idcategoria)
                .ToListAsync();

            if (productos == null || productos.Count == 0)
            {
                return NotFound("No se encontraron productos para la categoría especificada.");
            }

            return Ok(productos);
        }
    }

  

}



