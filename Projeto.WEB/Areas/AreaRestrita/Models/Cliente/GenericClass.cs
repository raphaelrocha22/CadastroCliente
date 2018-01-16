using Projeto.DAL.Persistencia;
using Projeto.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public abstract class GenericClass
    {
        public string Cnpj { get; set; }
        public int Codun { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public EnderecoViewModel EnderecoCadastro { get; set; }
        public EnderecoViewModel EnderecoCobranca { get; set; }
        public EnderecoViewModel EnderecoEntrega { get; set; }
        public int IdSessao { get; set; }
        public int IdRepresentante { get; set; }
        public string NomeRepresentante { get; set; }

        public List<SelectListItem> ListagemRepresentante
        {
            get
            {
                var lista = new List<SelectListItem>();
                var d = new RepresentanteDAL();

                foreach (var r in d.ListaRepresentante(IdSessao))
                {
                    var item = new SelectListItem();
                    item.Value = r.IdRepresentante.ToString();
                    item.Text = r.Nome.ToString();
                    lista.Add(item);
                }
                return lista;
            }
        }

    }
}