using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_.Net_Core.Models;

namespace Inmobiliaria_.Net_Core.Controllers
{
    public class ContratosController : Controller
    {
        private readonly IRepositorioContrato repositorio;
        private readonly IConfiguration config;

        private readonly IRepositorioInquilino repositorioInquilino;
        private readonly IRepositorioInmueble repositorioInmueble;

        public ContratosController(IConfiguration config,IRepositorioContrato repositorioContrato, IRepositorioInquilino repositorioInquilino, IRepositorioInmueble repositorioInmueble)
        {
            this.config = config;
            this.repositorio = repositorioContrato;
            this.repositorioInquilino = repositorioInquilino;
            this.repositorioInmueble = repositorioInmueble;
        }

        // GET: Contratos
        public ActionResult Index()
        {
            try
            {
                var lista = repositorio.ObtenerTodos();
                ViewBag.Id = TempData["Id"];
                ViewBag.inquilinosList = repositorioInquilino.ObtenerTodos();
                ViewBag.inmueblesList = repositorioInmueble.ObtenerTodos();
                if (TempData.ContainsKey("Mensaje"))
                    ViewBag.Mensaje = TempData["Mensaje"];                    

                return View(lista);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Contratos/Details/5
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

        // GET: Contratos/Create
        public ActionResult Create()
        {
            try
            {
                 var inquilinosList = new RepositorioInquilino(config).ObtenerTodos(); 
                ViewBag.inquilinosList = inquilinosList;
                var inmueblelist = new RepositorioInmueble(config).ObtenerTodos(); 
                ViewBag.inmuebleList = inmueblelist;
                return View(new Contrato());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Contratos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contrato contrato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorio.Alta(contrato);
                    TempData["Mensaje"] = "Contrato creado correctamente";
                    return RedirectToAction(nameof(Index));
        }
                else
                {
                 var inquilinosList = new RepositorioInquilino(config).ObtenerTodos();
                 ViewBag.inquilinosList = inquilinosList;
                 var inmueblelist = new RepositorioInmueble(config).ObtenerTodos();
                 ViewBag.inmuebleList = inmueblelist;
                    return View(contrato);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Contratos/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);
                 var inquilinosList = new RepositorioInquilino(config).ObtenerTodos();
                 ViewBag.inquilinosList = inquilinosList;
                 var inmueblelist = new RepositorioInmueble(config).ObtenerTodos();
                 ViewBag.inmuebleList = inmueblelist;
                return View(entidad);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Contratos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Contrato entidad)
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
                 var inquilinosList = new RepositorioInquilino(config).ObtenerTodos();
                 ViewBag.inquilinosList = inquilinosList;
                 var inmueblelist = new RepositorioInmueble(config).ObtenerTodos();
                 ViewBag.inmuebleList = inmueblelist;
                    return View(entidad);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Contratos/Delete/5
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

        // POST: Contratos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Contrato entidad)
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