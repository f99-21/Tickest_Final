using System.ComponentModel.DataAnnotations;

namespace Tickest_Final.Models
{
    public class tecnico
    {
        [Key]
        public int id_tecnico { get; set; }
        public int id_usuario { get; set; }
        public int id_categoria { get; set; }

    }
}
