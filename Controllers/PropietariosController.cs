using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_.Net_Core.Models;

namespace Inmobiliaria_.Net_Core.Controllers
{
    public class PropietariosController : Controller
    {
        // 1. Cambiamos el tipo del campo a la INTERFAZ.
        // Ya no es 'RepositorioPropietario', sino 'IRepositorioPropietario'.
        private readonly IRepositorioPropietario repositorio;
       

        // 2. Pedimos la INTERFAZ en el constructor (Inyección de Dependencias).
        // .NET se encargará de darnos la instancia correcta de 'RepositorioPropietario'.
        public PropietariosController(IRepositorioPropietario repositorio)
        {
            // 3. Ya no creamos el repositorio aquí, solo lo recibimos y lo asignamos.
            this.repositorio = repositorio;
        }

        // GET: Propietarios
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
            catch (Exception)
            {
                throw;
            }
        }

        // GET: Propietarios/Buscar/5
		[Route("[controller]/Buscar/{q}", Name = "Buscar")]
		public IActionResult Buscar(string q)
		{
			try
			{
				var res = repositorio.BuscarPorNombre(q);
				return Json(new { Datos = res });
			}
			catch (Exception ex)
			{
				return Json(new { Error = ex.Message });
			}
		}

        // GET: Propietarios/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);
                return View(entidad);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: Propietarios/Create
        public ActionResult Create()
        {
            try
            {
                return View(new Propietario());
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: Propietarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario propietario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorio.Alta(propietario);
                    TempData["Id"] = propietario.IdPropietario;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(propietario);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: Propietarios/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);
                return View(entidad);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: Propietarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Propietario entidad)
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
            catch (Exception)
            {
                throw;
            }
        }

        // GET: Propietarios/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);
                return View(entidad);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: Propietarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Propietario entidad)
        {
            try
            {
                repositorio.Baja(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}