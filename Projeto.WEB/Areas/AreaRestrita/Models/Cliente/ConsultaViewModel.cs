using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class ConsultaViewModel:GenericClass
    {
        public int IdCliente { get; set; }
        public int CodCliente { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string DataCadastro { get; set; }
        public string Classe { get; set; }

    }
}