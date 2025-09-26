using Npgsql;

namespace AplicativoChamados.Context {
	public class ConexaoBD {
		private static readonly string _strCon = "User ID=postgres;Password=VamosVencer2011.;Host=172.25.99.184;Port=5432;Database=atividaden2;Pooling=true;";
		public static NpgsqlConnection GetConexao() {
			var conexao = new NpgsqlConnection(_strCon);
			conexao.Open();
			return conexao;
		}
	}
}
