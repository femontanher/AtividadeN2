using AplicativoChamados.DAO;
using AplicativoChamados.Models;
using Microsoft.AspNetCore.Mvc;

namespace AplicativoChamados.Controllers
{
    public class ChamadosController : Controller{
		private readonly ChamadosDAO _chamadosDAO;
		private readonly UsuariosDAO _usuariosDAO;

		public ChamadosController(ChamadosDAO chamadosDAO, UsuariosDAO usuariosDAO) {
			_chamadosDAO = chamadosDAO;
			_usuariosDAO = usuariosDAO;
		}

        public IActionResult Index(){
			var chamados = _chamadosDAO.Listagem();

			foreach(var chamado in chamados) {
				var usuario = _usuariosDAO.ProcurarPorId(chamado.usuarioId);
				chamado.NomeDoUsuario = usuario?.nomeCompleto;
			}

			return View(chamados);
        }
		public IActionResult CriarChamado() {
			return View();
		}
		public IActionResult Salvar() {
			return View();
		}

		public IActionResult Detalhes() {
			return View();
		}

        public IActionResult Visualizar() {
			return View();
		}

		public IActionResult Alterar() {
			return View();
		}

		public IActionResult Edit() {
			return View();
		}

		public IActionResult Remover() {
			return View();
		}
	}
}
