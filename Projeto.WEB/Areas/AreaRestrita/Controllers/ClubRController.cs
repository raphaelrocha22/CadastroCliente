using Projeto.Entidades;
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

        public JsonResult MetaMinima(CadastroViewModel model)
        {
            var listaMetas = GenericClass.Modalidade_MediaMinima_MetaMinima();
            var mediaMetaMinima = listaMetas.Where(m => m.ModalidadeClubR.Equals(model.ModalidadeClubR)).ToList();

            return Json(mediaMetaMinima);
        }

        [HttpPost]
        public JsonResult Cadastro(CadastroViewModel model)
        {
            try
            {
                var c = new ClubR();
                c.Programa = "ClubR";
                c.Codun = model.Codun;
                c.NumeroContrato = model.NumeroContrato;
                c.NomeResponsavel = model.NomeResponsavel;
                c.CpfResponsavel = model.CpfResponsavel;
                c.Modalidade = model.ModalidadeClubR;
                c.DataNegociacao = model.DataNegociacao;
                c.DataInicio = model.DataInicioContrato;
                c.DataFim = model.DataFimContrato;
                c.MediaHistorica = model.MediaHistorica;
                c.PeriodoMeses = model.PeriodoMeses;
                c.MetaPeriodo = model.MetaPeriodo;
                c.Desconto = model.Desconto;
                c.Crescimento = Convert.ToDecimal(model.CrescimentoProposto);
                c.PrazoPagamento = model.PrazoPagamento;
                c.RebateValor = model.RebateValor;
                c.Ativo = model.Ativo;
                c.Programa = model.Programa;
                c.Contrato = $"{c.Programa}-{model.Codun}-{model.NumeroContrato}";

                string pasta = HttpContext.Server.MapPath("/Imagens/ClubR/");
                model.Contrato.SaveAs(pasta + c.Contrato);

                return Json("mensagem");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}