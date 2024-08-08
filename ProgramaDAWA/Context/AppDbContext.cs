using Microsoft.EntityFrameworkCore;
using ProgramaDAWA.Models;

namespace ProgramaDAWA.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().HasKey(c => c.Id);// Definir clave primaria para Categoria

            modelBuilder.Entity<Categoria>().Property(c => c.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Producto>()
            .Property(p => p.PrecioUnitario)
            .HasColumnType("decimal(18, 2)"); // Cambia el tamaño y escala según tus necesidades


        }

    }
}
