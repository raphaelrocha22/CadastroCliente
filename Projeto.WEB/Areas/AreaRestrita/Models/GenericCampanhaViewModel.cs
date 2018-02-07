using Projeto.Entidades;
using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Models
{
    public class GenericCampanhaViewModel
    {
        [RegularExpression("^[0-9]+$", ErrorMessage = "Digite apenas números")]
        [Range(1, int.MaxValue, ErrorMessage = "Campo Obrigatório")]
        public int Codun { get; set; }

        public int NumeroContrato { get; set; }

        [RegularExpression("^[a-zA-Zà-üÀ-Ü0-9\\s]{4,50}$", ErrorMessage = "Preencha com letras e números, máximo 50 caracteres")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string RazaoSocial { get; set; }

        [RegularExpression("^[a-zA-Zà-üÀ-Ü\\s]{4,50}$", ErrorMessage = "Digite apenas letas, máximo 50 caracteres")]
        public string NomeResponsavel { get; set; }

        [RegularExpression("^[-.0-9\\s]{11,14}$", ErrorMessage = "Digite apenas números com 11 dígitos")]
        public string CpfResponsavel { get; set; }

        public string DataNegociacao { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string DataInicioContrato { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string DataFimContrato { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int PeriodoMeses { get; set; }

        [RegularExpression("^[0-9.]+$", ErrorMessage = "Digite apenas números")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string MediaHistorica { get; set; }

        [RegularExpression("^[0-9.]+$", ErrorMessage = "Digite apenas números")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string MetaPeriodo { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string CrescimentoProposto { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int MesesPagamentoRBR { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int MesesPagamentoNetline { get; set; }

        [Range(2.52, int.MaxValue, ErrorMessage = "Campo Obrigatório, valor mínimo 2,52")]
        public decimal MarkUP { get; set; }

        public string Desconto { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public Guelta Guelta { get; set; }

        [RegularExpression("^[a-zA-Zà-üÀ-Ü0-9\\s]{1,1000}$", ErrorMessage = "Preencha com até 1000 caractéres")]
        public string Obervacao { get; set; }


        public List<SelectListItem> ListaPrazoPagamentoRBR
        {
            get
            {
                var lista = new List<SelectListItem>
                {
                    new SelectListItem(){Value = "1",Text="30 Dias"},
                    new SelectListItem(){Value = "2",Text="2 Vezes"},
                    new SelectListItem(){Value = "3",Text="3 Vezes"},
                    new SelectListItem(){Value = "4",Text="4 Vezes"},
                    new SelectListItem(){Value = "5",Text="5 Vezes"},
                    new SelectListItem(){Value = "6",Text="6 Vezes"},
                    new SelectListItem(){Value = "7",Text="7 Vezes"},
                    new SelectListItem(){Value = "8",Text="8 Vezes"},
                    new SelectListItem(){Value = "9",Text="9 Vezes"}
                };
                return lista;
            }
        }

        public List<SelectListItem> ListaPrazoPagamentoNetLine
        {
            get
            {
                var lista = new List<SelectListItem>
                {
                    new SelectListItem(){Value = "0",Text="Não habilitado compra Netline"},
                    new SelectListItem(){Value = "1",Text="30 Dias"},
                    new SelectListItem(){Value = "4",Text="4 Vezes"},
                };
                return lista;
            }
        }

        public Usuario usuario
        {
            get
            {
                Usuario u = (Usuario)HttpContext.Current.Session["usuario"];

                ///Bug apenas no pc do desenvolvedor :(
                if (u == null)
                {
                    u = new Usuario();
                    u.IdUsuario = 2;
                    u.Nome = "Raphael Rocha";
                }

                return u;
            }
        }
    }
}