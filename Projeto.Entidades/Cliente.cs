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
        public int idCliente { get; set; }
        public int codCliente { get; set; }
        public int codun { get; set; }
        public string razaoSocial { get; set; }
        public string nomeFantasia { get; set; }
        public string cnpj { get; set; }
        public string inscricaoEstadual { get; set; }
        public string inscricaoMunicipal { get; set; }
        public string classe { get; set; }

        public List<Endereco> endereco { get; set; }
        public Representante representante { get; set; }

    }
}
