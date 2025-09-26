using AplicativoChamados.Context;
using Npgsql;
using System.Data;

namespace AplicativoChamados.DAO {
	public class HelperDAO {
		public static DataTable ExecutaSelect(string sql, NpgsqlParameter[] parametros) {
			using (NpgsqlConnection conexao = ConexaoBD.GetConexao()) {
				using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conexao)) {
					if (parametros != null)
						adapter.SelectCommand.Parameters.AddRange(parametros);

					DataTable tabela = new DataTable();
					adapter.Fill(tabela);
					conexao.Close();
					return tabela;
				}
			}
		}
		public static void ExecutaSQL(string sql, NpgsqlParameter[] parametros) {
			using (NpgsqlConnection conexao = ConexaoBD.GetConexao()) {
				using (NpgsqlCommand comando = new NpgsqlCommand(sql, conexao)) {
					if (parametros != null)
						comando.Parameters.AddRange(parametros);
					comando.ExecuteNonQuery();
				}
			}
		}
	}
}
