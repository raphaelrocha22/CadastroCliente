using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.Entidades.Enuns;
using Projeto.Util;
using Projeto.WEB.Areas.AreaRestrita.Models.ClubR;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
            return View(new CadastroViewModel());
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

        public JsonResult MetaMinima(CadastroViewModel model)
        {
            var listaMetas = GenericClass.Modalidade_MediaMinima_MetaMinima();
            var metaMinimaMensal = listaMetas.Where(m => m.ModalidadeClubR.Equals(model.ModalidadeClubR)).ToList();

            return Json(metaMinimaMensal);
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

                    c.Campanha = Campanha.ClubR;
                    c.Codun = model.Codun;
                    c.NumeroContrato = model.NumeroContrato;
                    c.NomeResponsavel = model.NomeResponsavel;
                    c.CpfResponsavel = model.CpfResponsavel;
                    c.Modalidade = model.ModalidadeClubR;
                    c.DataNegociacao = model.DataNegociacao != null ? Convert.ToDateTime(model.DataNegociacao) : (DateTime)SqlDateTime.MinValue;
                    c.DataInicio = Convert.ToDateTime(model.DataInicioContrato);
                    c.DataFim = Convert.ToDateTime(model.DataFimContrato);
                    c.MediaHistorica = Convert.ToDecimal(model.MediaHistorica.Replace(".", ""));
                    c.PeriodoMeses = model.PeriodoMeses;
                    c.MetaPeriodo = Convert.ToDecimal(model.MetaPeriodo.Replace(".", ""));
                    c.Markup = model.MarkUP;
                    c.Desconto = 1 - (2.52M / c.Markup);
                    c.Crescimento = Convert.ToDecimal(model.CrescimentoProposto.Replace("%", "").Replace(".", ","));
                    c.MesesPagamentoRBR = model.MesesPagamentoRBR;
                    c.NetlineHabilitado = model.MesesPagamentoNetline != 0;
                    c.MesesPagamentoNetline = model.MesesPagamentoNetline;
                    c.RebatePercent = Convert.ToDecimal(model.RebatePercent.Replace("%","")) / 100;
                    c.RebateValor = c.MetaPeriodo * c.RebatePercent;
                    c.Guelta = model.Guelta;
                    c.Status = Status.Pendente;
                    c.Observacao = model.Obervacao;
                    c.Contrato = model.Contrato is null ? null : $"{c.Campanha}-{c.Codun}-{c.NumeroContrato}";
                    c.usuario.IdUsuario = model.usuario.IdUsuario;
                    c.usuario.Nome = model.usuario.Nome;

                    var d = new ClubRDAL();
                    d.Cadastrar(c);

                    if (model.Contrato != null)
                    {
                        string pasta = HttpContext.Server.MapPath("/Imagens/ClubR/");
                        string extesao = Path.GetExtension(model.Contrato.FileName);
                        model.Contrato.SaveAs(pasta + c.Contrato + extesao);
                    }

                    var r = new RepresentanteDAL();
                    List<string> destinatarios = r.ListaDestinatarios(c.usuario.IdUsuario);

                    Email.EnviarEmailClubR(c, destinatarios);

                    TempData["Sucesso"] = true;
                    TempData["Resultado"] = "Solicitação de Cadastro enviada com sucesso. \n" +
                        "Um E-mail de confirmação foi enviado, assim que o cliente estiver cadastrado você receberá uma confirmação via E-mail";
                    return RedirectToAction("Cadastro", "ClubR");                    
                }
                catch (Exception e)
                {
                    TempData["Sucesso"] = false;
                    TempData["Resultado"] = "Erro: " + e.Message;
                }
            }
            return View(new CadastroViewModel());
        }
    }
}