using AplicativoChamados.Models;
using Npgsql;
using System.Data;

namespace AplicativoChamados.DAO {
	
	public class UsuariosDAO {
		public bool IdExiste(int id) {
			string sql = "SELECT COUNT(*) FROM usuarios WHERE id = @id";
			var parametros = new[] { new NpgsqlParameter("@id", id) };
			DataTable tabela = HelperDAO.ExecutaSelect(sql, parametros);
			return Convert.ToInt32(tabela.Rows[0][0]) > 0;
		}

		public void Inserir(Usuario user) {
			if (IdExiste(user.usuarioId))
				throw new Exception("ID já existe. Não é possível inserir duplicado.");

			string sql = "INSERT INTO usuarios (id, nome) VALUES (@usuarioId, @nome)";

			NpgsqlParameter[] parametros = new NpgsqlParameter[] {
				new NpgsqlParameter("@usuarioId", NpgsqlTypes.NpgsqlDbType.Integer) {Value = user.usuarioId},
				new NpgsqlParameter("@nome", NpgsqlTypes.NpgsqlDbType.Varchar) {Value = user.nomeCompleto}
			};

			HelperDAO.ExecutaSQL(sql, parametros);
		}

		public List<Usuario> Listagem() {
			string sql = "SELECT id, nome FROM usuarios";

			DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

			List<Usuario> usuarios = new List<Usuario>();

			foreach(DataRow row in tabela.Rows) {
				var user = new Usuario {
					usuarioId = Convert.ToInt32(row[@"id"]),
					nomeCompleto = row["nome"].ToString()
				};

				usuarios.Add(user);
			}

			return usuarios;
		}

		public Usuario ProcurarPorId(int usuarioId) {
			string sql = "SELECT id, nome FROM usuarios WHERE id = @usuarioId";
			var parametros = new[] { new NpgsqlParameter("@usuarioId", usuarioId) };
			DataTable tabela = HelperDAO.ExecutaSelect(sql, parametros);

			if (tabela.Rows.Count == 0)
				return null;

			DataRow row = tabela.Rows[0];
			return new Usuario {
				usuarioId = Convert.ToInt32(row["id"]),
				nomeCompleto = row["nome"].ToString()
			};
		}

		public void Alterar(Usuario usuario) {
			string sql = "UPDATE usuarios SET id = @usuarioId, nome = @nomeCompleto WHERE id = @usuarioId";

			NpgsqlParameter[] parametros = new NpgsqlParameter[] {
				new NpgsqlParameter("@usuarioId", usuario.usuarioId),
				new NpgsqlParameter("@nomeCompleto", usuario.nomeCompleto)
			};

			HelperDAO.ExecutaSQL(sql, parametros);
		}

		public void Remover(int usuarioId) {
			string sql = "DELETE FROM usuarios WHERE id = @usuarioId";
			HelperDAO.ExecutaSQL(sql, new NpgsqlParameter[] {
				new NpgsqlParameter("@usuarioId", usuarioId)
			});
		}

		public int ProximoId() {
			int novoId = 1;
			string sql = "SELECT MAX(id) FROM usuarios";
			DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

			if (tabela.Rows[0][0] != DBNull.Value)
				novoId = Convert.ToInt32(tabela.Rows[0][0]) + 1;

			// Verifica se já existe
			while (IdExiste(novoId)) {
				novoId++;
			}

			return novoId;
		}
	}
}
