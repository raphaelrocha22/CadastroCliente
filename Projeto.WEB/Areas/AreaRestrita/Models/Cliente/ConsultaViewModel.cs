using System;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class ConsultaViewModel:GenericClass
    {
        public int IdCliente { get; set; }
        public int CodCliente { get; set; }
        public int Codun { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public int IdRepresentante { get; set; }
        public string NomeRepresentante { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }


        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public EnderecoViewModel EnderecoCadastro { get; set; }
        public EnderecoViewModel EnderecoCobranca { get; set; }
        public EnderecoViewModel EnderecoEntrega { get; set; }

        /// string por causa da exibição na tela via Jquery
        public string DataCadastro { get; set; }
        public string Classe { get; set; }

    }
}