using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.Entidades.Enum;
using Projeto.WEB.Areas.AreaRestrita.Models.ClubR;
using System;
using System.Collections.Generic;
using System.IO;
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

        public JsonResult NumeroContrato(CadastroViewModel model)
        {
            var d = new ClubRDAL();
            int numeroContrato = d.NumeroContrato(model.Codun);
            return Json(numeroContrato);
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

        public JsonResult MetaMinima(CadastroViewModel model)
        {
            var listaMetas = GenericClass.Modalidade_MediaMinima_MetaMinima();
            var mediaMetaMinima = listaMetas.Where(m => m.ModalidadeClubR.Equals(model.ModalidadeClubR)).ToList();

            return Json(mediaMetaMinima);
        }

        [HttpPost]
        public ActionResult Cadastro(CadastroViewModel model)
        {
            if (ModelState.IsValid)
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
                    c.MediaHistorica = Convert.ToDecimal(model.MediaHistorica.Replace(".", ""));
                    c.PeriodoMeses = model.PeriodoMeses;
                    c.MetaPeriodo = Convert.ToDecimal(model.MetaPeriodo.Replace(".", ""));
                    c.Desconto = Convert.ToDecimal(model.Desconto.Replace(".",","));
                    c.Crescimento = Convert.ToDecimal(model.CrescimentoProposto.Replace("%", "").Replace(".", ","));
                    c.PrazoPagamento = model.PrazoPagamento;
                    c.RebatePercent = Convert.ToDecimal(model.RebatePercent.Replace("%","")) / 100;
                    c.RebateValor = c.MetaPeriodo * c.RebatePercent;
                    c.Ativo = true;
                    c.Observacao = model.Obervacao;
                    c.Contrato = $"{c.Programa}-{c.Codun}-{c.NumeroContrato}";

                    var d = new ClubRDAL();
                    d.Cadastrar(c);

                    string pasta = HttpContext.Server.MapPath("/Imagens/ClubR/");
                    string extesao = Path.GetExtension(model.Contrato.FileName);
                    model.Contrato.SaveAs(pasta + c.Contrato + extesao);

                    ModelState.Clear();
                    ViewBag.Mensagem = $"Cliente {model.RazaoSocial}, CODUN {model.Codun} cadastrado com sucesso no programa ClubR";                  
                                        
                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = "Erro: " + e.Message;
                }
            }
            return View();
        }
    }
}