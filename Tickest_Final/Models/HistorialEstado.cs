using System;

namespace ProyectoFinal.Models
{
    public class HistorialEstado
    {
        public int Id_Estado { get; set; }

        public int Id_Ticket { get; set; }

        public string Estado { get; set; }

        public DateTime Fecha_Actualizacion { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
