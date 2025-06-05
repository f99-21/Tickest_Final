using Tickest_Final.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace GestionTickets.Controllers
{
    public class TicketController : Controller
    {
        private readonly ticketsContext _ticketsContexto;

        public TicketController(ticketsContext ticketsContexto)
        {
            _ticketsContexto = ticketsContexto;
        }

        // Vista de Crear Ticket (Formulario)
        [HttpGet]
        public IActionResult CrearTicket()
        {
            return View(); // Muestra el formulario para crear un nuevo ticket
        }

        // Acción para crear un nuevo ticket (POST)
        [HttpPost]
        public async Task<IActionResult> CrearTicket(TicketRequest request)
        {
            // Validaciones básicas
            if (string.IsNullOrEmpty(request.titulo) || string.IsNullOrEmpty(request.descripcion))
            {
                ViewBag.ErrorMessage = "El título y la descripción son obligatorios.";
                return View(request); // Devuelve la vista con el mensaje de error
            }

            if (request.usuario_id <= 0)
            {
                ViewBag.ErrorMessage = "Se requiere el usuario_id.";
                return View(request);
            }

            if (request.categoria_id <= 0)
            {
                ViewBag.ErrorMessage = "Se requiere el categoria_id.";
                return View(request);
            }

            if (request.archivos != null && request.archivos.Count > 5)
            {
                ViewBag.ErrorMessage = "No se pueden adjuntar más de 5 archivos.";
                return View(request);
            }

            // Crear el ticket
            var nuevoTicket = new ticket
            {
                titulo = request.titulo,
                descripcion = request.descripcion,
                tipo_ticket = request.tipo_ticket,
                prioridad = request.prioridad,
                id_usuario = request.usuario_id,
                id_categoria = request.categoria_id,
                fecha_creacion = System.DateTime.Now.AddSeconds(-System.DateTime.Now.Second).AddMilliseconds(-System.DateTime.Now.Millisecond),
                estado = "A" // Estado inicial: Abierto
            };

            // Guardar el ticket en la base de datos
            _ticketsContexto.ticket.Add(nuevoTicket);
            await _ticketsContexto.SaveChangesAsync();

            // Guardar los archivos adjuntos si se proporcionan
            if (request.archivos != null && request.archivos.Any())
            {
                foreach (var archivo in request.archivos)
                {
                    var archivoAdjunto = new archivo_adjunto
                    {
                        nombre_archivo = archivo.nombre,
                        ruta_archivo = archivo.url,
                        id_ticket = nuevoTicket.id_ticket
                    };

                    _ticketsContexto.archivo_adjunto.Add(archivoAdjunto);
                }

                await _ticketsContexto.SaveChangesAsync();
            }

            // Redirigir a la vista de detalles del ticket (o puedes mostrar un mensaje de éxito)
            return RedirectToAction("TicketCreado", new { id = nuevoTicket.id_ticket });
        }

        // Vista para mostrar el ticket creado
        [HttpGet]
        public IActionResult TicketCreado(int id)
        {
            var ticket = _ticketsContexto.ticket.FirstOrDefault(t => t.id_ticket == id);

            if (ticket == null)
            {
                return NotFound(); // Si el ticket no se encuentra, devolver 404
            }

            return View(ticket); // Pasamos el ticket a la vista
        }

        // Modelos para recibir el cuerpo del formulario
        public class TicketRequest
        {
            public string titulo { get; set; }
            public string descripcion { get; set; }
            public int categoria_id { get; set; }
            public string prioridad { get; set; }
            public string tipo_ticket { get; set; }
            public int usuario_id { get; set; }
            public List<ArchivoRequest> archivos { get; set; }
        }

        public class ArchivoRequest
        {
            public string nombre { get; set; }
            public string url { get; set; }
        }
    }
}
