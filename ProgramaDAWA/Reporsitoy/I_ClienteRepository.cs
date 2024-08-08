using ProgramaDAWA.Context;
using ProgramaDAWA.Models;
using Microsoft.EntityFrameworkCore;

namespace ProgramaDAWA.Repository
{
    public interface I_ClienteRepository
    {
        Task<List<Cliente>> GetAll();
        Task<Cliente> GetById(int id);
        Task<Cliente> GetByCedula(string cedula);
        Task<Cliente> Create(Cliente cliente);
        Task<Cliente> Update(Cliente cliente);
        Task Delete(int id);
        Task<bool> SaveChanges();
    }

    public class ClienteRepository : I_ClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAll()
        {
            List<Cliente> lstClientes = _context.Clientes.ToList();
            return lstClientes;
        }

        public async Task<Cliente> GetById(int id)
        {
            Cliente cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            return cliente;
        }

        public async Task<Cliente> GetByCedula(string cedula)
        {
            Cliente cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Cedula == cedula);
            return cliente;
        }

        public async Task<Cliente> Create(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> Update(Cliente cliente)
        {
            var existingCliente = await _context.Clientes.FindAsync(cliente.Id);

            if (existingCliente != null)
            {
                _context.Entry(existingCliente).CurrentValues.SetValues(cliente);
                await _context.SaveChangesAsync();
                return existingCliente;
            }
            else
            {
                throw new InvalidOperationException("El Cliente con el Id especificado no existe en el contexto.");
            }
        }

        public async Task Delete(int id)
        {
            var existingCliente = await _context.Clientes.FindAsync(id);

            if (existingCliente != null)
            {
                _context.Clientes.Remove(existingCliente);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("El Cliente con el Id especificado no existe en el contexto.");
            }
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }   

    }
}
