namespace ProyectoFinal.Models
{
    public class UsuarioRol
    {
        public int Id_Usuario { get; set; }

        public int Id_Rol { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Rol Rol { get; set; }
    }
}
