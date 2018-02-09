using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public abstract class GenericCampanha
    {
        public int Id { get; set; }
        public Campanha Campanha { get; set; }
        public int Codun { get; set; }
        public int NumeroContrato { get; set; }
        public string NomeResponsavel { get; set; }
        public string CpfResponsavel { get; set; }
        public DateTime DataNegociacao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal MediaHistorica { get; set; }
        public int PeriodoMeses { get; set; }
        public decimal MetaPeriodo { get; set; }
        public decimal Desconto { get; set; }
        public decimal Markup { get; set; }
        public decimal Crescimento { get; set; }
        public int MesesPagamentoRBR { get; set; }
        public int MesesPagamentoNetline { get; set; }
        public bool NetlineHabilitado { get; set; }
        public Status Status { get; set; }
        public string Observacao { get; set; }
        public Guelta Guelta { get; set; }

        public Usuario Usuario { get; set; }
    }
}
