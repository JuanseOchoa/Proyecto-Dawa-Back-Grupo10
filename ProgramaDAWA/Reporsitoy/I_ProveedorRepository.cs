using ProgramaDAWA.Context;
using ProgramaDAWA.Models;
using Microsoft.EntityFrameworkCore;

namespace ProgramaDAWA.Repository
{
    public interface I_ProveedorRepository
    {
        Task<List<Proveedor>> GetAll();
        Task<Proveedor> GetById(int id);
        Task<Proveedor> GetByCode(string code);
        Task<Proveedor> Create(Proveedor proveedor);
        Task<Proveedor> Update(Proveedor proveedor);
        Task Delete(int id);
        Task<bool> SaveChanges();
    }

    public class ProveedorRepository : I_ProveedorRepository
    {
        private readonly AppDbContext _context;

        public ProveedorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Proveedor>> GetAll()
        {
            List<Proveedor> lstProveedores = _context.Proveedores.ToList();
            return lstProveedores;
        }
        
        public async Task<Proveedor> GetById(int id)
        {
            Proveedor proveedor = await _context.Proveedores.FirstOrDefaultAsync(x => x.Id == id);
            return proveedor;
        }

        public async Task<Proveedor> GetByCode(string code)
        {
            Proveedor proveedor = await _context.Proveedores.FirstOrDefaultAsync(x => x.Codigo == code);
            return proveedor;
        }

        public async Task<Proveedor> Create(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        public async Task<Proveedor> Update(Proveedor proveedor)
        {
            var existingProveedor = await _context.Proveedores.FindAsync(proveedor.Id);

            if (existingProveedor != null)
            {
                _context.Entry(existingProveedor).CurrentValues.SetValues(proveedor);
                await _context.SaveChangesAsync();
                return existingProveedor;
            }
            else
            {
                throw new InvalidOperationException("El Proveedor con el Id especificado no existe en el contexto.");
            }
        }

        public async Task Delete(int id)
        {
            var existingProveedor = await _context.Proveedores.FindAsync(id);

            if (existingProveedor != null)
            {
                _context.Proveedores.Remove(existingProveedor);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("El Proveedor con el Id especificado no existe en el contexto.");
            }
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }


    }


}
