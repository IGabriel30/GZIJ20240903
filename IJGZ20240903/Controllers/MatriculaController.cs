using Microsoft.AspNetCore.Authorization;
using IJGZ20240903.Models;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IJGZ20240903.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//con authorize solo los usuarios autenticados podran acceder a sus acciones
    public class MatriculaController : ControllerBase
    {
         static List<Matricula> matriculas = new List<Matricula>();

        [HttpPost]
        public IActionResult CrearMatricula(Matricula matricula)
        {
  
            matriculas.Add(matricula);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult ModificarMatricula(int id, Matricula matricula)
        {
            var matriculaExistente = matriculas.FirstOrDefault(m => m.Id == id);
            if (matriculaExistente == null)
            {
                return NotFound();
            }

            // Actualizar los datos de la matrícula
            matriculaExistente.Alumno = matricula.Alumno;
            matriculaExistente.Curso = matricula.Curso;
            matriculaExistente.Turno = matricula.Turno;
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerMatriculaPorId(int id)
        {
            var matricula = matriculas.FirstOrDefault(m => m.Id == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return Ok(matricula);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Matricula>> ObtenerTodasLasMatriculas()
        {
            return Ok(matriculas);
        }
    }

}
