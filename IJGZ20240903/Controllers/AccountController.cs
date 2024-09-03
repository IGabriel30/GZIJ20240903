using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IJGZ20240903.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
         [HttpPost("login")]
        public IActionResult Login(string login, string password)
        {
            // Comprueba si las credenciales son válidas
            if (login == "admin" && password == "1234567")
            {
                //crea lista de reclamaciones
                var claims = new List<Claim>
                {
                    
                    new Claim(ClaimTypes.Name, login)
                };
                //crea una identidad de reclamaciones con el esquema de autenticacion por cookies
                var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                //crea propiedades de autenticacion adicionales
                var authProperties = new AuthenticationProperties
                {
                    // se puede configurar propiedades adicionales aquí
                    //para que persista
                    IsPersistent = true,
                };

                // Inicia sesión del usuario
                HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   new ClaimsPrincipal(claimsIdentity), authProperties);


                return Ok("Inició sesión correctamente.");
            }
            else
            {

                return Ok("Credenciales Incorrectas.");

            }
        }

        // POST api/<AccountController>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            //cierra sesion de ususairo
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Cerró sesión correctamente.");
        }
    }
}
