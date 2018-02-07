using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public class ClubR:GenericCampanha
    {
        public ModalidadeClubR Modalidade { get; set; }
        public decimal RebateValor { get; set; }
        public decimal RebatePercent { get; set; }
        public string Contrato { get; set; }        
    }
}
