using CrudBasico.Data;
using CrudBasico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CrudBasico.Controllers
{
    public class InicioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InicioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Contactos.ToListAsync());
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Contacto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.FechaCreacion = DateTime.Now;
            _context.Contactos.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos.FindAsync(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Contacto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var contacto = await _context.Contactos.AsNoTracking().FirstOrDefaultAsync(c => c.Id == model.Id);

            if (contacto.Id != model.Id)
            {
                return NotFound();
            }

            model.FechaCreacion = contacto.FechaCreacion;
            _context.Contactos.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos.FindAsync(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        public async Task<IActionResult> Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos.FindAsync(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarContacto(int? id)
        {

            var contacto = await _context.Contactos.FindAsync(id);

            if (contacto == null)
            {
                return NotFound();
            }

            _context.Contactos.Remove(contacto);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
