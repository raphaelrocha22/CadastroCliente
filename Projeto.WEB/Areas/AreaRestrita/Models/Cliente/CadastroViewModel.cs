using Projeto.Entidades.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class CadastroViewModel : GenericClass
    {
        [Range(1, int.MaxValue, ErrorMessage = "Selecione a Classe")]
        public ClasseCliente Classe { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int IdRepresentante { get; set; }

        [RegularExpression("^([0-9]{2}\\.?[0-9]{3}\\.?[0-9]{3}\\/?[0-9]{4}\\-?[0-9]{2})$", ErrorMessage = "CNPJ Inválido, informe apenas números com 14 caracteres")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Cnpj { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Insira apenas números")]
        public int Codun { get; set; }

        public int IdTransacao { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string RazaoSocial { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo {1} caracteres")]
        public string NomeFantasia { get; set; }

        [RegularExpression("^[0-9.-]{8,11}$", ErrorMessage = "Formato inválido, informe apenas números com 8 caractéres")]
        public string InscricaoEstadual { get; set; }

        [RegularExpression("^[0-9.-]{8,11}$", ErrorMessage = "Formato inválido, informe apenas números com 8 caractéres")]
        public string InscricaoMunicipal { get; set; }

        public EnderecoViewModel EnderecoCadastro { get; set; }
        public EnderecoViewModel EnderecoCobranca { get; set; }
        public EnderecoViewModel EnderecoEntrega { get; set; }

        public string Observacao { get; set; }

        public bool CobrancaIgualCadastro { get; set; }
        public bool EntregaIgualCadastro { get; set; }
        public bool EntregaIgualCobranca { get; set; }

        // Status da requisicao do CNPJ (OK ou ERRO)
        public string Status { get; set; }
    }
}