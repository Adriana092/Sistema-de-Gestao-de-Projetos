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
    public class EdicaoController : Controller
    {
        private readonly SistemaProjetoContext _context;

        public EdicaoController(SistemaProjetoContext context)
        {
            _context = context;
        }

        // GET: Edicao
        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return _context.Edicaos != null ? 
                          View(await _context.Edicaos.ToListAsync()) :
                          Problem("Entity set 'SistemaProjetoContext.Edicaos'  is null.");
        }

        // GET: Edicao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Edicaos == null)
            {
                return NotFound();
            }

            var edicao = await _context.Edicaos
                .FirstOrDefaultAsync(m => m.IdEdicao == id);
            if (edicao == null)
            {
                return NotFound();
            }

            return View(edicao);
        }

        // GET: Edicao/Create
        public IActionResult Create()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: Edicao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEdicao,Nome,Descricao,ValidoDe,ValidoAte")] Edicao edicao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(edicao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(edicao);
        }

        // GET: Edicao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Edicaos == null)
            {
                return NotFound();
            }

            var edicao = await _context.Edicaos.FindAsync(id);
            if (edicao == null)
            {
                return NotFound();
            }
            return View(edicao);
        }

        // POST: Edicao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEdicao,Nome,Descricao,ValidoDe,ValidoAte")] Edicao edicao)
        {
            if (id != edicao.IdEdicao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(edicao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EdicaoExists(edicao.IdEdicao))
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
            return View(edicao);
        }

        // GET: Edicao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Edicaos == null)
            {
                return NotFound();
            }

            var edicao = await _context.Edicaos
                .FirstOrDefaultAsync(m => m.IdEdicao == id);
            if (edicao == null)
            {
                return NotFound();
            }

            return View(edicao);
        }

        // POST: Edicao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Edicaos == null)
            {
                return Problem("Entity set 'SistemaProjetoContext.Edicaos'  is null.");
            }
            var edicao = await _context.Edicaos.FindAsync(id);
            if (edicao != null)
            {
                _context.Edicaos.Remove(edicao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EdicaoExists(int id)
        {
          return (_context.Edicaos?.Any(e => e.IdEdicao == id)).GetValueOrDefault();
        }

        public IActionResult CriarEdicao()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarEdicao(Edicao edicao)
        {
            if (edicao.Nome != null)
            {
                Edicao edicaoTemp = new Edicao();

                edicaoTemp.Descricao = edicao.Descricao;
                edicaoTemp.Nome = edicao.Nome;
                edicaoTemp.ValidoAte = edicao.ValidoAte;
                edicaoTemp.ValidoDe= edicao.ValidoDe;
                
                

                _context.Edicaos.Add(edicaoTemp);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
