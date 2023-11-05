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
    public class AlunoController : Controller
    {
        private readonly SistemaProjetoContext _context;

        public AlunoController(SistemaProjetoContext context)
        {
            _context = context;
        }

        // GET: Aluno
        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return _context.Alunos != null ? 
                          View(await _context.Alunos.ToListAsync()) :
                          Problem("Entity set 'SistemaProjetoContext.Alunos'  is null.");
        }

        // GET: Aluno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alunos == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(m => m.IdAluno == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // GET: Aluno/Create
        public IActionResult Create()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: Aluno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAluno,Username,Nome,Curso,Email,NumeroMec,Pass")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {

                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Aluno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Alunos == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        // POST: Aluno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAluno,Username,Nome,Curso,Email,NumeroMec,Pass")] Aluno aluno)
        {
            if (id != aluno.IdAluno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.IdAluno))
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
            return View(aluno);
        }

        // GET: Aluno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            if (id == null || _context.Alunos == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(m => m.IdAluno == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alunos == null)
            {
                return Problem("Entity set 'SistemaProjetoContext.Alunos'  is null.");
            }
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
          return (_context.Alunos?.Any(e => e.IdAluno == id)).GetValueOrDefault();
        }

        public IActionResult CriarAluno()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarAluno(Aluno aluno)
        {
            if (aluno.Nome != null)
            {
                Aluno alunoTemp = new Aluno();

                alunoTemp.Username = aluno.Username;
                alunoTemp.Nome = aluno.Nome;
                alunoTemp.Email = aluno.Email;
                alunoTemp.Curso = aluno.Curso;
                alunoTemp.NumeroMec = aluno.NumeroMec;
                alunoTemp.Pass = aluno.Pass;
             ;

                _context.Alunos.Add(alunoTemp);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
