using Projeto.DAL.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class GenericClass
    {
        public string classe { get; set; }

        public string cnpj { get; set; }

        public int codun { get; set; }

        public string razaoSocial { get; set; }

        public string nomeFantasia { get; set; }

        public string inscricaoEstadual { get; set; }

        public string inscricaoMunicipal { get; set; }

        public EnderecoViewModel enderecoCadastro { get; set; }

        public EnderecoViewModel enderecoCobranca { get; set; }

        public EnderecoViewModel enderecoEntrega { get; set; }

        public int idSessao { get; set; }

        public int idRepresentante { get; set; }

        public string nomeRepresentante { get; set; }

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