using AplicativoChamados.Models;
using Npgsql;
using System.Data;

namespace AplicativoChamados.DAO {
	public class ChamadosDAO {


		public bool IdExiste(int id) {
			string sql = "SELECT COUNT(*) FROM usuarios WHERE id = @id";
			var parametros = new[] { new NpgsqlParameter("@id", id) };
			DataTable tabela = HelperDAO.ExecutaSelect(sql, parametros);
			return Convert.ToInt32(tabela.Rows[0][0]) > 0;
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

		public void Inserir(Chamado chamado) {
			if (IdExiste(chamado.chamadoId))
				throw new Exception("ID já existe. Não é possível inserir duplicado.");

			string sql = "INSERT INTO chamados (id, data_abertura,descricao_problema, descricao_atendimento, data_atendimento, situacao, usuario_id) VALUES (@chamadoid, @data_abertura, @descricao_problema, @descricao_atendimento, @data_atendimento, @situacao, @usuarioid)";

			NpgsqlParameter[] parametros = new NpgsqlParameter[] {
				new NpgsqlParameter("@usuarioId", NpgsqlTypes.NpgsqlDbType.Integer) {Value = chamado.usuarioId}
			};

			HelperDAO.ExecutaSQL(sql, parametros);
		}
	}
}
