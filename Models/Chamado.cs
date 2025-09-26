using System.ComponentModel.DataAnnotations.Schema;

namespace AplicativoChamados.Models {
	public class Chamado {
		public int chamadoId { get; set; }
		public DateTime dataAbertura { get; set; }
		public DateTime dataAtendimento { get; set; }
		public string descricaoDoChamado { get;set; }
		public string descicaoAtendimento { get; set; }
		public string situacao { get; set; }
		public int usuarioId { get; set; }

		[NotMapped] 
		public string NomeDoUsuario { get; set; }

	}
}
