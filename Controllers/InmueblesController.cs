using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_.Net_Core.Models;

namespace Inmobiliaria_.Net_Core.Controllers
{
    public class InmueblesController : Controller
    {
        private readonly IRepositorioInmueble repositorioInmueble;
        private readonly IRepositorioPropietario repositorioPropietario;
        private readonly IConfiguration config;

        public InmueblesController(IConfiguration config, IRepositorioInmueble repositorioInmueble, IRepositorioPropietario repositorioPropietario)
        {
            this.config = config;
            this.repositorioInmueble = repositorioInmueble;
            this.repositorioPropietario = repositorioPropietario;
        }

        // GET: Inmuebles
        public ActionResult Index()
        {
            try
            {
                var lista = repositorioInmueble.ObtenerTodos();
                ViewBag.Id = TempData["Id"];
                ViewBag.propietarioList = repositorioPropietario.ObtenerTodos();
                if (TempData.ContainsKey("Mensaje"))
                    ViewBag.Mensaje = TempData["Mensaje"];
                return View(lista);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Inmuebles/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var entidad = repositorioInmueble.ObtenerPorId(id);
                return View(entidad);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Inmuebles/Create
        public IActionResult Create()
        {
            try
            {
                var propietariosList = repositorioPropietario.ObtenerTodos(); // Cargar la lista de propietarios
                ViewBag.propietariosList = propietariosList;

                var inmueble = new Inmueble
                {
                    IdPropietario = 0, // Inicializar con un valor predeterminado
                    Duenio = null // Inicializar como null si no hay propietario seleccionado
                };

                return View(inmueble);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Inmuebles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorioInmueble.Alta(inmueble);
                    TempData["Mensaje"] = "Inmueble creado correctamente";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var propietariosList = new RepositorioPropietario(config).ObtenerTodos();
                    ViewBag.propietariosList = propietariosList;
                    return View(inmueble);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Inmuebles/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var inmueble = repositorioInmueble.ObtenerPorId(id);
                if (inmueble == null)
                {
                    return NotFound();
                }

                inmueble.Duenio = repositorioPropietario.ObtenerPorId(inmueble.IdPropietario); // Cargar el propietario relacionado
                ViewBag.propietariosList = repositorioPropietario.ObtenerTodos();

                return View(inmueble);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Inmuebles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inmueble inmueble)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorioInmueble.Modificacion(inmueble); 
                    TempData["Mensaje"] = "Inmueble actualizado correctamente";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var propietariosList = repositorioPropietario.ObtenerTodos(); 
                    ViewBag.propietariosList = propietariosList;
                    return View(inmueble);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Inmuebles/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var entidad = repositorioInmueble.ObtenerPorId(id);
                return View(entidad);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Inmuebles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inmueble entidad)
        {
            try
            {
                repositorioInmueble.Baja(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}