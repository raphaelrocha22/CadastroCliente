using Projeto.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public int CodCliente { get; set; }
        public int Codun { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public ClasseCliente Classe { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }

        public List<Endereco> Enderecos { get; set; }
        public Representante Representante { get; set; }

    }
}
