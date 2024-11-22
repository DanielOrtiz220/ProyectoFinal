using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Data;
using ProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.Infrastructure;


namespace ProyectoFinal.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AppDBContext _appDBContext;

        public AccesoController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(ViewModels.UsuarioVM modelo)
        {
            if (modelo.clave != modelo.confirmarclave)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            Usuario usuario = new Usuario()
            {
                nombre = modelo.nombre,
                correo = modelo.correo,
                clave = modelo.clave,

            };

            Usuario? usuario_encontrado = await _appDBContext.Usuarios
                                            .Where(u =>
                                                u.correo == modelo.correo
                                            ).FirstOrDefaultAsync();

            if (usuario_encontrado == null)
            {
                await _appDBContext.Usuarios.AddAsync(usuario);
                await _appDBContext.SaveChangesAsync();

                if (usuario.Id != 0) return RedirectToAction("Login", "Acceso");            
                
            }
            else
            {
                  ViewData["Mensaje"] = "Correo Existente";
            }
          

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ViewModels.LoginVM modelo)
        {
            
            Usuario? usuario_encontrado = await _appDBContext.Usuarios
                                            .Where(u => 
                                                u.correo == modelo.correo && 
                                                u.clave == modelo.clave
                                            ).FirstOrDefaultAsync();

            string? correoUsuario = usuario_encontrado?.correo;

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias. ";
                return View();
            }
            else
            {
                if (correoUsuario == "jacob.ortizrivera@gmail.com")
                {
                    return RedirectToAction("Lista", "Acceso");
                }
                else
                {
                    return RedirectToAction("Formulario", "Acceso");
                }
            }

            
        }

        [HttpGet]
        public IActionResult Formulario()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Formulario(ViewModels.EventosVM modelo)
        {
            Eventos eventos = new Eventos()
            {
                Nombre_Completo = modelo.Nombre_Completo,
                Correo = modelo.Correo,
                Telefono = modelo.Telefono,
                DateOnly = modelo.DateOnly,
                CantidadInvitados = modelo.CantidadInvitados,


            };

            await _appDBContext.Eventos.AddAsync(eventos);
            await _appDBContext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Eventos> lista = await _appDBContext.Eventos.ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            Eventos eventos = await _appDBContext.Eventos.FirstAsync(e => e.Id == id);

            _appDBContext.Eventos.Remove(eventos);
            
            await _appDBContext.SaveChangesAsync();

            return RedirectToAction(nameof(Lista));
        }
    }
}
