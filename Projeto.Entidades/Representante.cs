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
        public int idRepresentante { get; set; }
        public string nome { get; set; }
        public TipoRepresentante tipoRepresentante { get; set; }
        public bool ativo { get; set; }
    }
}
