using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class EnderecoViewModel
    {
        public int IdEndereco { get; set; }
        public string Tipo { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; } 
        public string Municipio { get; set; } 
        public string UF { get; set; }
        public string Cep { get; set; }
        public string Email { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string DataCadastro { get; set; }
    }
}