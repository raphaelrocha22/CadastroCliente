using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Areas.AreaRestrita.Models.CentralSolicitacao
{
    public class ConsultaViewModel
    {
        public int Id { get; set; }
        public string Tabela { get; set; }
        public string Campanha { get; set; }
        public int CodCliente { get; set; }
        public int Codun { get; set; }
        public string Usuario { get; set; }
        public Acao Acao { get; set; }
    }
}