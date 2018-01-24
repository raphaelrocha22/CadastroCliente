using Projeto.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Projeto.WEB.Areas.AreaRestrita.Models.ClubR
{
    public class CadastroViewModel
    {
        [RegularExpression("^[0-9]+$", ErrorMessage = "Digite apenas números")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int Codun { get; set; }

        [RegularExpression("^[a-zA-Zà-üÀ-Ü\\s]{4,50}$", ErrorMessage = "Digite apenas letas com até 50 caracteres")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string RazaoSocial { get; set; }

        [RegularExpression("^[a-zA-Zà-üÀ-Ü\\s]{4,50}$", ErrorMessage = "Digite apenas letas com até 50 caracteres")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string NomeResponsavel { get; set; }

        [RegularExpression("^[-.0-9\\s]{11,14}$", ErrorMessage = "Digite apenas números")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string CpfResponsavel { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime DataNegociacao { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public HttpPostedFileBase Contrato { get; set; }

        [RegularExpression("^[a-zA-Zà-üÀ-Ü0-9\\s]{4,1000}$", ErrorMessage = "Preencha com até 1000 caractéres")]
        public string Obervacao { get; set; }

        public int NumeroContrato { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime DataInicioContrato { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime DataFimContrato { get; set; }

        [RegularExpression("^[0-9.]+$", ErrorMessage = "Digite apenas números")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string MediaHistorica { get; set; }

        [RegularExpression("^[0-9.]+$", ErrorMessage = "Digite apenas números")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string MetaPeriodo { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string CrescimentoProposto { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string PrazoPagamento { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Desconto { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string RebatePercent { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int PeriodoMeses { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public ModalidadeClubR ModalidadeClubR { get; set; }
    }
}