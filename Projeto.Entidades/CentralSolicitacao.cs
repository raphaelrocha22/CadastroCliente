using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public class CentralSolicitacao
    {
        public int Id { get; set; }
        public string Tabela { get; set; }
        public Campanha Campanha { get; set; }
        public int CodCliente { get; set; }
        public int Codun { get; set; }
        public StatusSolicitacao Status { get; set; }
        public string NomeUsuario { get; set; }
        public Acao Acao { get; set; }
    }
}
