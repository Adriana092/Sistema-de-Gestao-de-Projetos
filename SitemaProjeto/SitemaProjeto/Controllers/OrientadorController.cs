using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SitemaProjeto.Data;
using SitemaProjeto.Models;

namespace SitemaProjeto.Controllers
{
    public class OrientadorController : Controller
    {
        private readonly SistemaProjetoContext _context;

        public OrientadorController(SistemaProjetoContext context)
        {
            _context = context;
        }

        // GET: Orientador
        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return _context.Orientadors != null ? 
              View(await _context.Orientadors.ToListAsync()) :
              Problem("Entity set 'SistemaProjetoContext.Orientadors'  is null.");
        }

        // GET: Orientador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orientadors == null)
            {
                return NotFound();
            }

            var orientador = await _context.Orientadors
                .FirstOrDefaultAsync(m => m.IdOrientador == id);
            if (orientador == null)
            {
                return NotFound();
            }

            return View(orientador);
        }

        // GET: Orientador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orientador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrientador,Username,Nome,Email,Instituicao,Pass")] Orientador orientador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orientador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orientador);
        }

        // GET: Orientador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orientadors == null)
            {
                return NotFound();
            }

            var orientador = await _context.Orientadors.FindAsync(id);
            if (orientador == null)
            {
                return NotFound();
            }
            return View(orientador);
        }

        // POST: Orientador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrientador,Username,Nome,Email,Instituicao,Pass")] Orientador orientador)
        {
            if (id != orientador.IdOrientador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orientador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrientadorExists(orientador.IdOrientador))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orientador);
        }

        // GET: Orientador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            if (id == null || _context.Orientadors == null)
            {
                return NotFound();
            }

            var orientador = await _context.Orientadors
                .FirstOrDefaultAsync(m => m.IdOrientador == id);
            if (orientador == null)
            {
                return NotFound();
            }

            return View(orientador);
        }

        // POST: Orientador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orientadors == null)
            {
                return Problem("Entity set 'SistemaProjetoContext.Orientadors'  is null.");
            }
            var orientador = await _context.Orientadors.FindAsync(id);
            if (orientador != null)
            {
                _context.Orientadors.Remove(orientador);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrientadorExists(int id)
        {
          return (_context.Orientadors?.Any(e => e.IdOrientador == id)).GetValueOrDefault();
        }

        public IActionResult AdicionarOrientador()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarOrientador(Orientador orientador)
		{
            if(orientador.Nome != null)
			{
                Orientador oriTemp = new Orientador();

                oriTemp.Nome = orientador.Nome;
                oriTemp.Username = orientador.Username;
                oriTemp.Email = orientador.Email;
                oriTemp.Pass = orientador.Pass;
                oriTemp.Instituicao = orientador.Instituicao;

                _context.Orientadors.Add(oriTemp);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
		}
    }
}
