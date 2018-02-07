using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Markup
{
    public class CadastroViewModel:GenericCampanhaViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Selecione a Modalidade")]
        public ModalidadeMarkup ModalidadeMarkup { get; set; }

        public List<SelectListItem> ListaPeriodo
        {
            get
            {
                var lista = new List<SelectListItem>
                {
                    new SelectListItem(){Value = "3",Text="Trimestral"}
                };
                return lista;
            }
        }

        public List<SelectListItem> ListaGuelta
        {
            get
            {
                var lista = new List<SelectListItem>
                {
                    new SelectListItem(){Value = Guelta.Normal.ToString(), Text = Guelta.Normal.GetAttribute<DisplayAttribute>().Name},
                    new SelectListItem(){Value = Guelta.Sem_Guelta.ToString(), Text = Guelta.Sem_Guelta.GetAttribute<DisplayAttribute>().Name}

                };
                return lista;
            }
        }
    }
}