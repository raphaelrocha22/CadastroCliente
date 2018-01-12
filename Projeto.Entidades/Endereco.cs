using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public class Endereco
    {
        public int idEndereco { get; set; }
        public string tipo { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string municipio { get; set; }
        public string UF { get; set; }
        public string cep { get; set; }
        public string telefone1 { get; set; }
        public string telefone2 { get; set; }
        public string email { get; set; }
        public DateTime dataCadastro { get; set; }
    }
}
