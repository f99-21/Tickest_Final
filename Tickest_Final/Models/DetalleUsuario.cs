using System;

namespace ProyectoFinal.Models
{
    public class DetalleUsuario
    {
        public int Id_Detalle { get; set; }

        public DateTime Ultima_Actividad { get; set; }

        public string Area_Requerida { get; set; }

        public string Direccion { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
