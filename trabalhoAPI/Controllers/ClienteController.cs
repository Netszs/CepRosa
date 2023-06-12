using trabalhoAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trabalhoAPI.Models;
using System;

namespace trabalhoAPI.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allClientes = await _context.Clientes.ToListAsync();
            return View(allClientes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente, IList<IFormFile> Img)
        {
            IFormFile uploadedImage = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            if(Img.Count > 0)
            {
                uploadedImage.OpenReadStream().CopyTo(ms);
                cliente.Foto = ms.ToArray();
            }

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var produto = await _context.Clientes.FindAsync(id);
            if(produto == null)
            {
                return BadRequest();
            }

            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var produto = await _context.Clientes.FindAsync(id);
            if (produto == null)
            {
                return BadRequest();
            }

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Cliente cliente, IList<IFormFile> Img)
        {
            if(id == null)
            {
                return NotFound();
            }
            var dadosAntigos = _context.Clientes.AsNoTracking().FirstOrDefault(c=>c.Id==id);

            IFormFile uploadedImage = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            if (Img.Count > 0)
            {
                uploadedImage.OpenReadStream().CopyTo(ms);
                cliente.Foto = ms.ToArray();
            }
            else
            {
                cliente.Foto = dadosAntigos.Foto;
            }
            if(ModelState.IsValid)
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(cliente);
        }

        [HttpGet]
        public async Task<IActionResult> Delete (Guid? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return BadRequest();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
