using ProgramaDAWA.Context;
using ProgramaDAWA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ProgramaDAWA.Repository
{
    public interface I_CategoriaRepository
    {
        Task<List<Categoria>> GetAll();
        Task<Categoria> GetById(int id);
        Task<Categoria> Create(Categoria categoria);
        Task<Categoria> Update(Categoria categoria);
        Task Delete(int id);
        Task<bool> SaveChanges();
    }

    public class CategoriaRepository : I_CategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> GetAll()
        {
            List<Categoria> lstCategorias = await _context.Categorias.      ToListAsync();
            return lstCategorias;
        }

        public async Task<Categoria> GetById(int id)
        {
            Categoria categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
            return categoria;
        }

        public async Task<Categoria> Create(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> Update(Categoria categoria)
        {
            var existingCategoria = await _context.Categorias.FindAsync(categoria.Id);

            if (existingCategoria != null)
            {
                _context.Entry(existingCategoria).CurrentValues.SetValues(categoria);
                await _context.SaveChangesAsync();
                return existingCategoria;
            }
            else
            {
                throw new InvalidOperationException("La Categoria con el Id especificado no existe en el contexto.");
            }
        }

        public async Task Delete(int id)
        {
            var existingCategoria = await _context.Categorias.FindAsync(id);

            if (existingCategoria != null)
            {
                _context.Categorias.Remove(existingCategoria);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("La Categoria con el Id especificado no existe en el contexto.");
            }
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }

}
