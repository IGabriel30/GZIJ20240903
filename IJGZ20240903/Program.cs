//importar el using para la autenticación por cookies
using Microsoft.AspNetCore.Authentication.Cookies;
//importar using para trabajar con JSON
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//agregar servicios al contenedor de dependencias
//agregar el servicio de controladores al contenedor

builder.Services.AddControllers();
//se agrega el servicio para la exploracion de API de puntos finales
builder.Services.AddEndpointsApiExplorer();
//agrega servicio para la generacion de swagger
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        //configura el nombre del parámetro de URL para redireccionamiento no autorizado
        options.ReturnUrlParameter = "unauthorized";
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                //cambia el codigo de estado No autorizado
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                //establa el tipo de contenido como JSON(u otro formato)
                context.Response.ContentType = "application/json";

                var message = new
                {
                    error = "No Autorizado",
                    message="Debe iniciar sesion para acceder a este recurso."
                };

                //serializa el objeto 'message' en formato
                var jsonMessage = JsonSerializer.Serialize(message);
                //escribe el mensaje JSON en la respuesta HTTP
                return context.Response.WriteAsync(jsonMessage);
            }
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
