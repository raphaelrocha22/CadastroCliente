using Newtonsoft.Json;
using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class JsonCNPJ
    {
        [JsonProperty("nome")]
        public string razaoSocial { get; set; }

        [JsonProperty("fantasia")]
        public string nomeFantasia { get; set; }

        [JsonProperty("logradouro")]
        public string logradouro { get; set; }

        [JsonProperty("numero")]
        public string numero { get; set; }

        [JsonProperty("complemento")]
        public string complemento { get; set; }

        [JsonProperty("bairro")]
        public string bairro { get; set; }

        [JsonProperty("municipio")]
        public string municipio { get; set; }

        [JsonProperty("uf")]
        public string uf { get; set; }

        [JsonProperty("cep")]
        public string cep { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("telefone")]
        public string telefone1 { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }
    }
}