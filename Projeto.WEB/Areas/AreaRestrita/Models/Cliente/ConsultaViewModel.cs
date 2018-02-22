using System;
using System.ComponentModel.DataAnnotations;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class ConsultaViewModel:GenericClass
    {
        [Display(Name = "Cod Transacao")]
        public int IdCliente { get; set; }

        [Display(Name = "Cod Cliente")]
        public int CodCliente { get; set; }

        [Display(Name = "Codun")]
        public int Codun { get; set; }

        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [Display(Name = "Id Agente")]
        public int IdAgente { get; set; }

        [Display(Name = "Id Promotor")]
        public int IdPromotor { get; set; }

        [Display(Name = "Agente")]
        public string NomeAgente { get; set; }

        [Display(Name = "Promotor")]
        public string NomePromotor { get; set; }

        [Display(Name = "Inscrição Estadual")]
        public string InscricaoEstadual { get; set; }

        [Display(Name = "Inscrição Municipal")]
        public string InscricaoMunicipal { get; set; }
        
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public EnderecoViewModel EnderecoCadastro { get; set; }
        public EnderecoViewModel EnderecoCobranca { get; set; }
        public EnderecoViewModel EnderecoEntrega { get; set; }

        /// string por causa da exibição na tela via Jquery
        [Display(Name = "Data Cadastro")]
        public string DataCadastro { get; set; }

        [Display(Name = "Classe")]
        public string Classe { get; set; }
    }
}