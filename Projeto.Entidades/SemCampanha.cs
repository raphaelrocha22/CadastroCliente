using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public class SemCampanha
    {
        public int Id { get; set; }
        public Campanha Campanha { get; set; }
        public Cliente Cliente { get; set; }
        public int Versao { get; set; }
        public DateTime DataNegociacao { get; set; }
        public DateTime DataInicio { get; set; }
        public decimal Desconto { get; set; }
        public decimal Markup { get; set; }
        public int MesesPagamentoRBR { get; set; }
        public int MesesPagamentoNetline { get; set; }
        public bool NetlineHabilitado { get; set; }
        public StatusSolicitacao Status { get; set; }
        public string Observacao { get; set; }
        public Guelta Guelta { get; set; }

        public Acao Acao { get; set; }
        public Usuario Usuario { get; set; }
    }
}
