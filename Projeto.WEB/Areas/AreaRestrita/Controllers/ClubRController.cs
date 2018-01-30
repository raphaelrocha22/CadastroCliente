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
                    c.usuario = new Usuario();

                    c.Programa = "ClubR";
                    c.Codun = model.Codun;
                    c.NumeroContrato = model.NumeroContrato;
                    c.NomeResponsavel = model.NomeResponsavel;
                    c.CpfResponsavel = model.CpfResponsavel;
                    c.Modalidade = model.ModalidadeClubR;
                    if (model.DataNegociacao != DateTime.MinValue)
                        c.DataNegociacao = model.DataNegociacao;
                    c.DataInicio = model.DataInicioContrato;
                    c.DataFim = model.DataFimContrato;
                    c.MediaHistorica = Convert.ToDecimal(model.MediaHistorica.Replace(".", ""));
                    c.PeriodoMeses = model.PeriodoMeses;
                    c.MetaPeriodo = Convert.ToDecimal(model.MetaPeriodo.Replace(".", ""));
                    c.Desconto = Convert.ToDecimal(model.Desconto.Replace(".",","));
                    c.Markup = 2.52M / (1 - c.Desconto);
                    c.Crescimento = Convert.ToDecimal(model.CrescimentoProposto.Replace("%", "").Replace(".", ","));
                    c.PrazoPagamento = model.PrazoPagamento;
                    c.RebatePercent = Convert.ToDecimal(model.RebatePercent.Replace("%","")) / 100;
                    c.RebateValor = c.MetaPeriodo * c.RebatePercent;
                    c.Ativo = true;
                    c.Observacao = model.Obervacao;
                    c.Contrato = $"{c.Programa}-{c.Codun}-{c.NumeroContrato}";
                    c.usuario.IdUsuario = model.IdUsuario;

                    var d = new ClubRDAL();
                    d.Cadastrar(c);

                    string pasta = HttpContext.Server.MapPath("/Imagens/ClubR/");
                    string extesao = Path.GetExtension(model.Contrato.FileName);
                    model.Contrato.SaveAs(pasta + c.Contrato + extesao);

                    ModelState.Clear();
                    ViewBag.Mensagem = "Solicitação de Cadastro enviada com sucesso";         
                }
                catch (Exception e)
                {
                    ViewBag.Mesagem = "Erro: " + e.Message;
                }
            }
            return View(new CadastroViewModel() { DataNegociacao = DateTime.Today,
                DataInicioContrato = DateTime.Today, DataFimContrato = DateTime.Today});
        }
    }
}