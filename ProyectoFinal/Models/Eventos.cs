namespace ProyectoFinal.Models
{
    public class Eventos
    {
        public int Id { get; set; }
        public string Nombre_Completo { get; set; }
        public string Correo { get; set; }
        public int Telefono { get; set; }
        public DateOnly DateOnly { get; set; }
        public int CantidadInvitados { get; set; }

    }
}
