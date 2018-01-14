﻿using Newtonsoft.Json;
using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class ClienteViewModel
    {
        public int idCliente { get; set; }

        public int codCliente { get; set; }

        public int codun { get; set; }

        [JsonProperty("nome")]
        public string razaoSocial { get; set; }

        [JsonProperty("fantasia")]
        public string nomeFantasia { get; set; }

        public string cnpj { get; set; }

        public string inscricaoEstadual { get; set; }

        public string inscricaoMunicipal { get; set; }

        public string classe { get; set; }

        public string dataCadastro { get; set; }

        public DateTime dataInicio { get; set; }

        public DateTime dataFim { get; set; }

        public Endereco enderecoCadastro { get; set; }

        public Endereco enderecoCobranca { get; set; }

        public Endereco enderecoEntrega { get; set; }

        public Representante representante { get; set; }

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

        public int idSessao { get; set; }

        public List<SelectListItem> ListagemRepresentante
        {
            get
            {
                var lista = new List<SelectListItem>();
                var d = new RepresentanteDAL();
                
                foreach (var r in d.ListaRepresentante(idSessao))
                {
                    var item = new SelectListItem();
                    item.Value = r.idRepresentante.ToString();
                    item.Text = r.nome.ToString();
                    lista.Add(item);
                }
                return lista;
            }
        }
    }
}