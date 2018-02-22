using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public abstract class GenericClass
    {
        public Usuario usuario
        {
            get
            {
                Usuario u = (Usuario)HttpContext.Current.Session["usuario"];

                /////Bug apenas no pc do desenvolvedor :(
                //if (u == null)
                //{
                //    u = new Usuario();
                //    u.IdUsuario = 3;
                //    u.Nome = "Raphael Rocha";
                //}

                return u;
            }
        }

        public List<SelectListItem> ListagemRepresentante
        {
            get
            {
                var lista = new List<SelectListItem>();
                var d = new RepresentanteDAL();

                foreach (var r in d.ListaRepresentante(usuario.IdUsuario))
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