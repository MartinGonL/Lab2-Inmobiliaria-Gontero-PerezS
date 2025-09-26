using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_.Net_Core.Models;

namespace Inmobiliaria_.Net_Core.Controllers
{
    public class InquilinoController : Controller
    {
        private readonly IRepositorioInquilino repositorio;
        private readonly IConfiguration config;

        public InquilinoController(IConfiguration config, IRepositorioInquilino repositorio)
        {
            this.config = config;
            this.repositorio = repositorio;
        }

        // GET: Inquilino
        public ActionResult Index()
        {
            try
            {
                var lista = repositorio.ObtenerTodos();
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

        // GET: Inquilino/Details
        public ActionResult Details(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);
                return View(entidad);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Inquilino/Create
        public ActionResult Create()
        {
            try
            {              
                return View(new Inquilino());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Inquilino/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inquilino inquilino)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorio.Alta(inquilino);
                    TempData["Id"] = inquilino.IdInquilino;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(inquilino);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Inquilino/Edit
        public ActionResult Edit(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);
                return View(entidad);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Inquilino/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inquilino entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorio.Modificacion(entidad);
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

        [Route("[controller]/Buscar/{q?}", Name = "BuscarInquilinos")]
        public IActionResult Buscar(string q)
        {
        try
        {
        var res = repositorio.BuscarPorNombre(q);
        return Json(new
        {
            results = res.Select(i => new
            {
                id = i.IdInquilino,
                text = $"{i.Nombre} {i.Apellido}"
            })
        });
        }
        catch (Exception ex)
         {
        return Json(new { Error = ex.Message });
         }
        }

        // GET: Inquilino/Delete
        public ActionResult Delete(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);
                return View(entidad);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Inquilino/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inquilino entidad)
        {
            try
            {
                repositorio.Baja(id);
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