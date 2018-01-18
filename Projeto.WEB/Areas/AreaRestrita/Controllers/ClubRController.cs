using Projeto.Entidades.Enum;
using Projeto.WEB.Areas.AreaRestrita.Models.ClubR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Controllers
{
    [Authorize]
    public class ClubRController : Controller
    {
        // GET: AreaRestrita/ClubR
        public ActionResult Cadastro()
        {
            return View();
        }

        public JsonResult PrazosContrato(CadastroViewModel model)
        {
            var listaPrazos = GenericClass.Modalidade_PrazoContrato();
            var prazos = listaPrazos.Where(m => m.ModalidadeClubR.Equals(model.ModalidadeClubR)).ToList();

            return Json(prazos);
        }

        public JsonResult Descontos(CadastroViewModel model)
        {
            var listaDescontos = GenericClass.PrazoPagamento_Desconto();
            var descontos = listaDescontos.Where(m => m.PrazoPagamento.Equals(model.PrazoPagamento)).ToList();

            return Json(descontos);
        }

        public JsonResult Rebate(CadastroViewModel model)
        {
            decimal crescimento = Convert.ToDecimal(model.CrescimentoProposto);

            var listaRebates = GenericClass.Modalidade_Crescimento_Rebate();
            var rebate = listaRebates.Where(m => m.ModalidadeClubR.Equals(model.ModalidadeClubR) && crescimento >= m.crescimentoMinimo && crescimento <= m.crescimentoMaximo).ToList();

            return Json(rebate);
        }

        //public ActionResult Teste2()
        //{
        //    //var busca = GenericClass.Modalidade_Prazo().Where(c => c.PrazoPagamento.Equals("Anual")).ToList();

        //    var modalidade_Prazo = GenericClass.Modalidade_Prazo();
        //    var modalidade_Crescimento_Rebate = GenericClass.Modalidade_Crescimento_Rebate();
        //    var modalidade_MediaMinima_MetaMinima = GenericClass.Modalidade_MediaMinima_MetaMinima();

        //    var modalidade = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Ouro");
        //    var result_prazo = modalidade_Prazo.Where(m => m.ModalidadeClubR.Equals(modalidade)).ToList();
        //    var result_rebate = modalidade_Crescimento_Rebate.Where(m => m.ModalidadeClubR.Equals(modalidade)).ToList();
        //    var result_mediaMeta = modalidade_MediaMinima_MetaMinima.Where(m => m.ModalidadeClubR.Equals(modalidade)).ToList();
            
        //    return View();
        //}

        [HttpPost]
        public JsonResult Cadastro(CadastroViewModel model)
        {
            return Json("mensagem");
        }
    }
}