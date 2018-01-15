using Projeto.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public class Representante
    {
        public int IdRepresentante { get; set; }
        public string Nome { get; set; }
        public string TipoRepresentante { get; set; }
        public bool Ativo { get; set; }

        public List<Cliente> Clientes { get; set; }

        public Representante()
        {

        }

        public Representante(int idRepresentante)
        {
            IdRepresentante = idRepresentante;
        }
    }
}
