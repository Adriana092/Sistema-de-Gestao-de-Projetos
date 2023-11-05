using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SitemaProjeto.Data;
using SitemaProjeto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace SitemaProjeto.Controllers
{
    public class ProjetosController : Controller
    {
        private readonly SistemaProjetoContext _context;
        

        public ProjetosController(SistemaProjetoContext context)
        {
            _context = context;
        }

        // GET: Projetos
        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            var email = HttpContext.Session.GetString("Email");   
            var sistemaProjetoContext = _context.Projetos.Where(x => x.IdOrientadorNavigation.Email == email).OrderByDescending(x => x.IdProjeto).Include(p => p.IdEdicaoNavigation);
            return View(await sistemaProjetoContext.ToListAsync());
        }



        // GET: Projetos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            if (id == null || _context.Projetos == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projetos
                .Include(p => p.IdEdicaoNavigation)
                .Include(p => p.IdOrientadorNavigation)
                .FirstOrDefaultAsync(m => m.IdProjeto == id);
            if (projeto == null)
            {
                return NotFound();
            }

            return View(projeto);
        }

        // GET: Projetos/Create
        public IActionResult Create()
        {
            ViewData["IdEdicao"] = new SelectList(_context.Edicaos, "IdEdicao", "IdEdicao");
            ViewData["IdOrientador"] = new SelectList(_context.Orientadors, "IdOrientador", "IdOrientador");
            return View();
        }
        
        public IActionResult InserirProjeto()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Edicao = _context.Edicaos.Select(c => new SelectListItem()
           { Text = c.Nome, Value = c.IdEdicao.ToString() }).ToList();
           ViewBag.Orientador = _context.Orientadors.Select(c => new SelectListItem()
           { Text = c.Nome, Value = c.IdOrientador.ToString() }).ToList();

            return View();
        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InserirProjeto(Projeto projeto)
        {
            if (projeto.Titulo != null)
            {
                Projeto projTemp = new Projeto();

                projTemp.Titulo = projeto.Titulo;
                projTemp.NumAlunos = projeto.NumAlunos;
                projTemp.AreaInvestigacao = projeto.AreaInvestigacao;
                projTemp.CentroInvestigacao = projeto.CentroInvestigacao;
                projTemp.Apresentacao = projeto.Apresentacao;
                projTemp.Objetivos = projeto.Objetivos;
                projTemp.IdOrientador = projeto.IdOrientador;
                projTemp.IdEdicao = projeto.IdEdicao;
                projTemp.CoorientadorInterno = projeto.CoorientadorInterno;
                projTemp.CoorientadorExterno = projeto.CoorientadorExterno;

                _context.Projetos.Add(projTemp);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

    
       
        // POST: Projetos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProjeto,Titulo,NumAlunos,AreaInvestigacao,CentroInvestigacao,Apresentacao,Objetivos,IdOrientador,IdEdicao")] Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projeto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEdicao"] = new SelectList(_context.Edicaos, "IdEdicao", "IdEdicao", projeto.IdEdicao);
            ViewData["IdOrientador"] = new SelectList(_context.Orientadors, "IdOrientador", "IdOrientador", projeto.IdOrientador);
            return View(projeto);
        }

        // GET: Projetos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            if (id == null || _context.Projetos == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto == null)
            {
                return NotFound();
            }
            ViewBag.Edicao = _context.Edicaos.Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.IdEdicao.ToString() }).ToList();
            ViewData["IdOrientador"] = new SelectList(_context.Orientadors, "IdOrientador", "IdOrientador", projeto.IdOrientador);
            return View(projeto);
        }

        // POST: Projetos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEdicao")] Edicao edicao)
        {
            
            Projeto projeto1 = new Projeto();
            Projeto projeto2 = new Projeto();

            projeto1 = await _context.Projetos.FindAsync(id);
            projeto2.Titulo = projeto1.Titulo;
            projeto2.NumAlunos = projeto1.NumAlunos;
            projeto2.Apresentacao = projeto1.Apresentacao;
            projeto2.AreaInvestigacao = projeto1.AreaInvestigacao;
            projeto2.CentroInvestigacao = projeto1.CentroInvestigacao;
            projeto2.Objetivos = projeto1.Objetivos;
            projeto2.CoorientadorExterno = projeto1.CoorientadorExterno;
            projeto2.CoorientadorInterno = projeto1.CoorientadorInterno;
            projeto2.IdOrientador = projeto1.IdOrientador;

            if(projeto1 != null)
            {
                projeto2.IdEdicao = edicao.IdEdicao;
                

                _context.Projetos.Add(projeto2);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            
            return View();
        }

        // GET: Projetos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            if (id == null || _context.Projetos == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projetos
                .Include(p => p.IdEdicaoNavigation)
                .Include(p => p.IdOrientadorNavigation)
                .FirstOrDefaultAsync(m => m.IdProjeto == id);
            if (projeto == null)
            {
                return NotFound();
            }

            return View(projeto);
        }

        // POST: Projetos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Orientador orientador)
        {
            if (_context.Projetos == null)
            {
                return Problem("Entity set 'SistemaProjetoContext.Projetos'  is null.");
            }
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto != null)
            {
                _context.Projetos.Remove(projeto);

                var notificacao = new Notificacao
                {
                    Titulo = "Projeto Recusado",
                    Mensagem = $"O seu projeto '{projeto.Titulo}' foi recusado."
                };
                orientador.Notificacoes.Add(notificacao);


                HttpContext.Session.SetString("Notificacao", JsonConvert.SerializeObject(notificacao));
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Catalogo));
        }

        private bool ProjetoExists(int id)
        {
          return (_context.Projetos?.Any(e => e.IdProjeto == id)).GetValueOrDefault();
        }

       

      
        public async Task<IActionResult> Catalogo()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            if (role == "orientador" && ViewBag.Email == "lfb@utad.pt")
            {
                ViewBag.ShowAllButtons = true;
            }
            else
            {
                ViewBag.ShowAllButtons = false;
            }
            var sistemaProjetoContext = _context.Projetos.OrderByDescending(x => x.IdProjeto).Include(p => p.IdEdicaoNavigation).Include(p => p.IdOrientadorNavigation);
            return View(await sistemaProjetoContext.ToListAsync());
        }

        public async Task<IActionResult> InscricaoProjetos()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            var sistemaProjetoContext = _context.Projetos.OrderBy(x => x.IdProjeto).Include(p => p.IdEdicaoNavigation).Include(p => p.IdOrientadorNavigation);


            return View(await sistemaProjetoContext.ToListAsync());
        }


        public async Task<IActionResult> DetalhesLista(int? id)
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            if (id == null || _context.Projetos == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projetos
                .Include(p => p.IdEdicaoNavigation)
                .Include(p => p.IdOrientadorNavigation)
                .FirstOrDefaultAsync(m => m.IdProjeto == id);
            if (projeto == null)
            {
                return NotFound();
            }

            return View(projeto);
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            if (id == null || _context.Projetos == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projetos
                .Include(p => p.IdEdicaoNavigation)
                .Include(p => p.IdOrientadorNavigation)
                .FirstOrDefaultAsync(m => m.IdProjeto == id);
            if (projeto == null)
            {
                return NotFound();
            }

            return View(projeto);
        }

       
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminacaoConfirmada(int id)
        {
            if (_context.Projetos == null)
            {
                return Problem("Entity set 'SistemaProjetoContext.Projetos'  is null.");
            }
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto != null)
            {
                _context.Projetos.Remove(projeto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
