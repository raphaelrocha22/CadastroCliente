using Projeto.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Projeto.WEB.Areas.AreaRestrita.Models.ClubR
{
    public class CadastroViewModel:GenericClass
    {
        public string CrescimentoProposto { get; set; }

        public int Codun { get; set; }
        public int NumeroContrato { get; set; }
        public string NomeResponsavel { get; set; }
        public string CpfResponsavel { get; set; }

        public DateTime DataNegociacao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }



        
        

        public decimal RebateValor { get; set; }
        public bool Ativo { get; set; }
        public string Contrato { get; set; }
    }
}