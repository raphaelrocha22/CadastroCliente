using System;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class ConsultaViewModel:GenericClass
    {
        public int IdCliente { get; set; }
        public int CodCliente { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        /// <summary>
        /// string por causa da exibição na tela via Jquery
        /// </summary>
        public string DataCadastro { get; set; }
        public string Classe { get; set; }

    }
}