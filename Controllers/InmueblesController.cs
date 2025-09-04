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
        public ActionResult Create()
        {
            try
            {
                return View(new Inmueble());
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
                    TempData["Id"] = inmueble.IdInmueble;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(inmueble);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Inmuebles/Edit/5
        public ActionResult Edit(int id)
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

        // POST: Inmuebles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inmueble entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorioInmueble.Modificacion(entidad);
                    TempData["Mensaje"] = "Datos guardados correctamente";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(entidad);
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