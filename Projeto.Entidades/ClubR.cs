using Projeto.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public class ClubR
    {
        public int IdClubR { get; set; }
        public int Codun { get; set; }
        public int NumeroContrato { get; set; }
        public string NomeResponsavel { get; set; }
        public string CpfResponsavel { get; set; }
        public ModalidadeClubR Modalidade { get; set; }
        public DateTime DataNegociacao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal MediaHistorica { get; set; }
        public int PeriodoMeses { get; set; }
        public decimal MetaPeriodo { get; set; }
        public double Desconto { get; set; }
        public double Crescimento { get; set; }
        public string PrazoPagamento { get; set; }
        public decimal RebateValor { get; set; }
        public double RebatePercent { get; set; }
        public bool Ativo { get; set; }
        public string Contrato { get; set; }
    }
}
