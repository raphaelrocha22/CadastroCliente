using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class EnderecoViewModel
    {
        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("numero")]
        public string Numero { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("municipio")]
        public string Municipio { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("telefone")]
        public string Telefone1 { get; set; }

        public string Telefone2 { get; set; }
    }
}