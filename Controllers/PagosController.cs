using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Inmobiliaria_.Net_Core.Models;

public class PagosController : Controller
{
    private readonly RepositorioPago repoPago;
    private readonly RepositorioContrato repoContrato;

    public PagosController(IConfiguration config)
    {
        repoPago = new RepositorioPago(config);
        repoContrato = new RepositorioContrato(config);
    }

    // GET: Pagos
    public IActionResult Index()
    {
        var lista = repoPago.ObtenerTodos();
        return View(lista);
    }

    // GET: Pagos/Details/5
    public IActionResult Details(int id)
    {
        var pago = repoPago.ObtenerPorId(id);
        if (pago == null) return NotFound();
        return View(pago);
    }

    // GET: Pagos/Create
public IActionResult Create()
{
   
    ViewBag.Contratos = new SelectList(repoContrato.ObtenerTodos(),"IdContrato","Descripcion");
    return View();
}

// POST: Pagos/Create
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Pago pago)
{
    if (ModelState.IsValid)
    {
        repoPago.Alta(pago);
        return RedirectToAction(nameof(Index));
    }
    
    ViewBag.Contratos = new SelectList(
        repoContrato.ObtenerTodos(),
        "IdContrato",
        "Descripcion",
        pago.IdContrato
    );
    return View(pago);
}

// GET: Pagos/Edit/5
public IActionResult Edit(int id)
{
    var pago = repoPago.ObtenerPorId(id);
    if (pago == null) return NotFound();

    ViewBag.Contratos = new SelectList(
        repoContrato.ObtenerTodos(),
        "IdContrato",
        "Descripcion",
        pago.IdContrato
    );
    return View(pago);
}

// POST: Pagos/Edit/5
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(int id, Pago pago)
{
    if (id != pago.IdPago) return NotFound();

    if (ModelState.IsValid)
    {
        repoPago.Modificacion(pago);
        return RedirectToAction(nameof(Index));
    }

    ViewBag.Contratos = new SelectList(
        repoContrato.ObtenerTodos(),
        "IdContrato",
        "Descripcion",
        pago.IdContrato
    );
    return View(pago);
}

    // GET: Pagos/Delete/5
    public IActionResult Delete(int id)
    {
        var pago = repoPago.ObtenerPorId(id);
        if (pago == null) return NotFound();
        return View(pago);
    }

    // POST: Pagos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        repoPago.Baja(id);
        return RedirectToAction(nameof(Index));
    }
}
