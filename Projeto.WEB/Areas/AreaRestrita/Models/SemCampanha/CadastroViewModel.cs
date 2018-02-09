using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.Entidades;
using Projeto.Entidades.Enuns;

namespace Projeto.WEB.Areas.AreaRestrita.Models.SemCampanha
{
    public class CadastroViewModel
    {
        public int IdTransacao { get; set; }
        public int CodCliente { get; set; }
        public int Codun { get; set; }
        public int NumeroContrato { get; set; }
        public string DataNegociacao { get; set; }

        [RegularExpression("^[a-zA-Zà-üÀ-Ü0-9\\s]{4,50}$", ErrorMessage = "Preencha com letras e números, máximo 50 caracteres")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string DataInicioContrato { get; set; }

        [Range(2.52, int.MaxValue, ErrorMessage = "Campo Obrigatório, valor mínimo 2,52")]
        public decimal MarkUP { get; set; }

        public string Desconto { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int MesesPagamentoRBR { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int MesesPagamentoNetline { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public Guelta Guelta { get; set; }

        [RegularExpression("^[a-zA-Zà-üÀ-Ü0-9\\s]{1,1000}$", ErrorMessage = "Preencha com até 1000 caractéres")]
        public string Obervacao { get; set; }

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