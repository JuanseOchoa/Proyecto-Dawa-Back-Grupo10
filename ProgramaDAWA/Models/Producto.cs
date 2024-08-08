using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProgramaDAWA.Models
{
    public class Producto
    {
        [Key]
        
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Categoria Categoria { get; set; }

        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
    }
}
