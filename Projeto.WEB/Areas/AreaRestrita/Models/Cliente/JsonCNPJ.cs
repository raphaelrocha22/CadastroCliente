using Newtonsoft.Json;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class JsonCNPJ
    {
        [JsonProperty("nome")]
        public string RazaoSocial { get; set; }

        [JsonProperty("fantasia")]
        public string NomeFantasia { get; set; }

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
        public string UF { get; set; }

        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("telefone")]
        public string Telefone1 { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}