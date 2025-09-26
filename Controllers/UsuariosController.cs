using AplicativoChamados.DAO;
using AplicativoChamados.Models;
using Microsoft.AspNetCore.Mvc;

namespace AplicativoChamados.Controllers
{
    public class UsuariosController : Controller
    {
		private readonly UsuariosDAO _usuariosDAO;

		public UsuariosController(UsuariosDAO usuariosDAO) {
			_usuariosDAO = usuariosDAO;
		}

        public IActionResult Index(){
			List<Usuario> usuarios = _usuariosDAO.Listagem();

            return View(usuarios);
        }

		[HttpGet]
		public IActionResult CriarUsuario() {
			var model = new Usuario();

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Salvar(Usuario user) {
			user.usuarioId = _usuariosDAO.ProximoId();

			if (ModelState.IsValid) {
				_usuariosDAO.Inserir(user);
				return RedirectToAction("Index");
			}

			return View("CriarUsuario", user);
		}


		[HttpGet]
		public IActionResult Visualizar(int usuarioId) {
			var usuario = _usuariosDAO.ProcurarPorId(usuarioId);
			if (usuario == null)
				return NotFound();

			return View(usuario);
		}

		public IActionResult Alterar(int usuarioId) {
			var usuario = _usuariosDAO.ProcurarPorId(usuarioId);

			if (usuario == null)
				NotFound();

			return View(usuario);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Atualizar(Usuario user) {
			if (ModelState.IsValid) {
				_usuariosDAO.Alterar(user);
				return RedirectToAction("Index");
			}
			return View(user);
		}

		public IActionResult Remover(int usuarioId) {
			_usuariosDAO.Remover(usuarioId);

			List<Usuario> usuarios = _usuariosDAO.Listagem();

			return View("Index", usuarios);
		}
	}
}
