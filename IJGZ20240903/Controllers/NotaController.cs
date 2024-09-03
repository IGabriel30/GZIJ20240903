using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IJGZ20240903.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IJGZ20240903.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private static List<Nota> notas = new List<Nota>();

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Nota> ObtenerNotas()
        {
            // Devuelve todas las notas, ya que este endpoint es público
            return notas;
        }

        [HttpPost]
        [Authorize]
        public IActionResult RegistrarNotas(Nota nota)
        {
            // Solo usuarios autenticados pueden registrar notas
            notas.Add(nota);
            return Ok();
        }

    }
}
