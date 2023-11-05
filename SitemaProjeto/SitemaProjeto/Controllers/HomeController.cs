using Microsoft.AspNetCore.Mvc;
using SitemaProjeto.Models;
using System.Diagnostics;
using SitemaProjeto.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SitemaProjeto.Controllers
{
    public class HomeController : Controller
    {
        private readonly SistemaProjetoContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(SistemaProjetoContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.ShowAddProjectButton = role == "orientador" || role == "gerente";
            ViewBag.ShowCandidaturaButton = role == "aluno";
            ViewBag.Role = role;
            ViewBag.Manager = role == "gerente";
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");


            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role); 

            if (role == "orientador" && ViewBag.Email == "abc@utad.pt")
            {
                ViewBag.ShowAllButtons = true;
            }
            else
            {
                ViewBag.ShowAllButtons = false;
            }

            if (HttpContext.Session.GetString("Notificacao") != null)
            {
                
                var notificacaoJson = HttpContext.Session.GetString("Notificacao");
                var notificacao = JsonConvert.DeserializeObject<Notificacao>(notificacaoJson);

                ViewBag.Notificacao = notificacao;

                HttpContext.Session.Remove("Notificacao");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        public async Task<IActionResult> Login(string email, string password)
        {
            string curso = "Engenharia Informática";

            if (email != null && password != null)
            {
                if (email.EndsWith("@alunos.utad.pt"))
                {
                    var alunoFromBDProjeto = await _context.Alunos.FirstOrDefaultAsync(a => a.Email == email && a.Pass == password && a.Curso == curso);
                    if (alunoFromBDProjeto != null)
                    {
                        HttpContext.Session.SetString("Email", alunoFromBDProjeto.Email);
                        HttpContext.Session.SetString("role", "aluno");
                        HttpContext.Session.SetString("UserName", alunoFromBDProjeto.Username);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else if (email.EndsWith("@utad.pt"))
                {
                    var orientadorFromBDProjeto = await _context.Orientadors.FirstOrDefaultAsync(o => o.Email == email && o.Pass == password);
                    if (orientadorFromBDProjeto != null)
                    {
                        HttpContext.Session.SetString("Email", orientadorFromBDProjeto.Email);
                        HttpContext.Session.SetString("role", "orientador");
                        HttpContext.Session.SetString("UserName", orientadorFromBDProjeto.Username);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else if (email == "abc@utad.pt")
                {
                    var orientadorFromBDProjeto = await _context.Orientadors.FirstOrDefaultAsync(o => o.Email == email && o.Pass == password);
                    if (orientadorFromBDProjeto != null)
                    {
                        HttpContext.Session.SetString("Email", orientadorFromBDProjeto.Email);
                        HttpContext.Session.SetString("role", "gerente");
                        HttpContext.Session.SetString("UserName", orientadorFromBDProjeto.Username);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult InserirNotas()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InserirNotas(Notas notas)
        {
            if (notas != null)
            {
                Notas notaTemp = new Notas();

                notaTemp.Numero_Mec = notas.Numero_Mec;
                notaTemp.Media = notas.Media;
                notaTemp.Num_Cadeiras = notas.Num_Cadeiras;

                _context.Notas.Add(notaTemp);
                _context.SaveChanges();
                return RedirectToAction("InscricaoProjetos", "Projetos");
            }
            return View();
        }
        public IActionResult EscolherGrupo()
        {
            var role = HttpContext.Session.GetString("role");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(role);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Edicao = _context.Edicaos.Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.IdEdicao.ToString() }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EscolherGrupo(Grupo grupo)
        {
            if (grupo.NumeroMec1 != null)
            {
                Grupo grupoTemp = new Grupo();

                grupoTemp.IdEdicao = grupo.IdEdicao;
                grupoTemp.NumeroMec1 = grupo.NumeroMec1;
                grupoTemp.NumeroMec2 = grupo.NumeroMec2;
                grupoTemp.NumeroMec3 = grupo.NumeroMec3;

                _context.Grupos.Add(grupoTemp);
                _context.SaveChanges();
                return RedirectToAction("InscricaoProjetos", "Projetos");
            }

            return View();
        }

    }
}