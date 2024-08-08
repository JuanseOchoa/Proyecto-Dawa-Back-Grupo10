using ProgramaDAWA.Context;
using ProgramaDAWA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ProgramaDAWA.Repository
{
    public interface I_ProductoRepository
    {
        Task<List<Producto>> GetAll();
        Task<Producto> GetById(int id);
        Task<Producto> GetByNombre(string Nombre);
        Task<IEnumerable<Producto>> GetByCategoriaNombre(string nombreCategoria);


        Task<ActionResult<Producto>> Create(Producto producto);
        Task<Producto> Update(Producto producto);
        Task Delete(int id);
        Task<bool> SaveChanges();
    }

  
    public class ProductoRepository : I_ProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> GetAll()
        {
            List<Producto> lstProductos = await _context.Productos
                .Include(p => p.Categoria)
                .ToListAsync(); // <-- Corrected position of ToListAsync
            return lstProductos;
        }

        public async Task<Producto> GetById(int id)
        {
            Producto producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(x => x.Id == id);
            return producto;
        }
        public async Task<Producto> GetByNombre(string Nombre)
        {
            Producto producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(x => x.Nombre == Nombre);
            return producto;
        }

        public async Task<IEnumerable<Producto>> GetByCategoriaNombre(string nombreCategoria)
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Where(p => p.Categoria.Nombre == nombreCategoria)
                .ToListAsync();

            return productos;
        }

        public async Task<ActionResult<Producto>> Create(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;

        }

        public async Task<Producto> Update(Producto producto)
        {
            var existingProducto = await _context.Productos.FindAsync(producto.Id);

            if (existingProducto != null)
            {
                _context.Entry(existingProducto).CurrentValues.SetValues(producto);
                await _context.SaveChangesAsync();
                return existingProducto;
            }
            else
            {
                throw new InvalidOperationException("El Producto con el Id especificado no existe en el contexto.");
            }
        }

        public async Task Delete(int id)
        {
            var existingProducto = await _context.Productos.FindAsync(id);

            if (existingProducto != null)
            {
                _context.Productos.Remove(existingProducto);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("El Producto con el Id especificado no existe en el contexto.");
            }
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}