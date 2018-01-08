using Newtonsoft.Json;
using Projeto.WEB.Areas.AreaRestrita.Models.JsonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class ClienteViewModel
    {
        public string clienteClasse { get; set; }
        public int codun { get; set; }
        public int codVendedor { get; set; }
        public string NumeroCNPJ { get; set; }

        [JsonProperty("atividade_principal")]
        public List<AtividadePrincipal> AtividadePrincipal { get; set; }

        [JsonProperty("atividades_secundarias")]
        public List<AtividadesSecundarias> AtividadesSecundarias { get; set; }

        [JsonProperty("data_situacao")]
        public string DataSituacao { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        [JsonProperty("telefone")]
        public string Telefone1 { get; set; }

        public string Telefone2 { get; set; }

        [JsonProperty("qsa")]
        public List<Qsa> Qsa { get; set; }

        [JsonProperty("situacao")]
        public string Situacao { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("numero")]
        public string Numero { get; set; }

        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("municipio")]
        public string Municipio { get; set; }

        [JsonProperty("abertura")]
        public string Abertura { get; set; }

        [JsonProperty("naturezaJuridica")]
        public string NaturezaJuridica { get; set; }

        [JsonProperty("fantasia")]
        public string Fantasia { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("ultima_atualizacao")]
        public DateTime UltimaAtualizacao { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("tipo")]
        public string Tipo { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("efr")]
        public string Efr { get; set; }

        [JsonProperty("motivo_situacao")]
        public string MotivoSituacao { get; set; }

        [JsonProperty("situacao_especial")]
        public string SituacaoEspecial { get; set; }

        [JsonProperty("data_situacao_especial")]
        public string DataSituacaoEspecial { get; set; }

        [JsonProperty("capital_social")]
        public decimal CapitalSocial { get; set; }

        [JsonProperty("extra")]
        public Extra Extra { get; set; }
    }
}