using System.ComponentModel.DataAnnotations;

namespace Tickest_Final.Models
{
    public class categoria
    {
        [Key]
        public int id_categoria { get; set; }
        public string nombre_categoria { get; set; } = null!;
    }
}
