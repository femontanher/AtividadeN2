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
				new NpgsqlParameter("@chamadoid", NpgsqlTypes.NpgsqlDbType.Integer) {Value = chamado.chamadoId},
				new NpgsqlParameter("@usuarioId", NpgsqlTypes.NpgsqlDbType.Integer) {Value = chamado.usuarioId},
				new NpgsqlParameter("@data_abertura", NpgsqlTypes.NpgsqlDbType.Date) {Value = chamado.dataAbertura},
				new NpgsqlParameter("@data_atendimento", NpgsqlTypes.NpgsqlDbType.Date) {Value = chamado.dataAtendimento},
				new NpgsqlParameter("@descricao_problema", NpgsqlTypes.NpgsqlDbType.Varchar) {Value = chamado.descricaoDoChamado},
				new NpgsqlParameter("@descricao_atendimento", NpgsqlTypes.NpgsqlDbType.Varchar) {Value = chamado.descicaoAtendimento},
				new NpgsqlParameter("@situacao", NpgsqlTypes.NpgsqlDbType.Varchar) {Value = chamado.situacao}
			};

			HelperDAO.ExecutaSQL(sql, parametros);
		}

		public List<Chamado> Listagem() {
			string sql = "SELECT * FROM chamados";

			DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

			List<Chamado> chamados = new List<Chamado>();

			foreach (DataRow row in tabela.Rows) {
				var chamado = new Chamado {
					chamadoId = Convert.ToInt32(row[@"id"]),
					usuarioId = Convert.ToInt32(row[@"usuario_id"]),
					dataAbertura = Convert.ToDateTime(row[@"data_abertura"]),
					dataAtendimento = Convert.ToDateTime(row[@"data_atendimento"]),
					descicaoAtendimento = row[@"descricao_atendimento"].ToString(),
					descricaoDoChamado = row[@"descricao_problema"].ToString(),
					situacao = row[@"situacao"].ToString()
				};

				chamados.Add(chamado);
			}

			return chamados;
		}
	}
}
