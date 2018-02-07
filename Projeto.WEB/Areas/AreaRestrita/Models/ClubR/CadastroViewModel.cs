using Projeto.Entidades.Enuns;
using Projeto.WEB.Validacao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Models.ClubR
{
    public class CadastroViewModel:GenericCampanhaViewModel
    {
        [UpdateImageValidator(ErrorMessage = "Formato do arquivo inválido, formatos aceitos: PDF, JPG, BMP e PNG de até 10 MB")]
        public HttpPostedFileBase Contrato { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string RebatePercent { get; set; }

        public string RebateValor { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione a Modalidade")]
        public ModalidadeClubR ModalidadeClubR { get; set; }

        public List<SelectListItem> ListaRebate
        {
            get
            {
                var lista = new List<SelectListItem>
                {
                    new SelectListItem(){Value = "0.03",Text="3%"},
                    new SelectListItem(){Value = "0.04",Text="4%"},
                    new SelectListItem(){Value = "0.05",Text="5%"},
                };
                return lista;
            }
        }

        public List<SelectListItem> ListaPeriodo
        {
            get
            {
                var lista = new List<SelectListItem>
                {
                    new SelectListItem(){Value = "3",Text="Trimestral"},
                    new SelectListItem(){Value = "6",Text="Semestral"},
                    new SelectListItem(){Value = "12",Text="Anual"},
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