using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.Entidades.Enuns;
using Projeto.Util;
using Projeto.WEB.Areas.AreaRestrita.Models.Markup;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Controllers
{
    [Authorize]
    public class MarkupController : Controller
    {
        // GET: AreaRestrita/Markup
        public ActionResult Cadastro()
        {
            return View(new CadastroViewModel());
        }

        public JsonResult Versao(CadastroViewModel model)
        {
            var d = new MarkupDAL();
            int versao = d.Versao(model.Codun);
            return Json(versao);
        }

        public JsonResult MetaMinima(CadastroViewModel model)
        {
            var listaMetas = RegrasMarkup.Modalidade_MediaMinima_MetaMinima();
            var metaMinimaMensal = listaMetas.Where(m => m.ModalidadeMarkup.Equals(model.ModalidadeMarkup)).ToList();

            return Json(metaMinimaMensal);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Cadastro(CadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var c = new Markup();
                    c.Usuario = new Usuario();

                    c.Campanha = Campanha.MarkUP;
                    c.Codun = model.Codun;
                    c.Versao = model.Versao;
                    c.NomeResponsavel = model.NomeResponsavel;
                    c.CpfResponsavel = model.CpfResponsavel;
                    c.Modalidade = model.ModalidadeMarkup;
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
                    c.Guelta = model.Guelta;
                    c.Status = StatusSolicitacao.Pendente;
                    c.Observacao = model.Obervacao;
                    c.Acao = Acao.Cadastrar;
                    c.Usuario = model.usuario;

                    var d = new MarkupDAL();
                    d.Cadastrar(c);

                    var r = new RepresentanteDAL();
                    List<string> destinatarios = r.ListaDestinatarios(c.Usuario.IdUsuario);

                    Email.EnviarEmailMarkup(c, destinatarios);

                    TempData["Sucesso"] = true;
                    TempData["Resultado"] = "Solicitação de Cadastro enviada com sucesso. \n" +
                        "Um E-mail de confirmação foi enviado, assim que o cliente estiver cadastrado você receberá uma confirmação via E-mail";
                    return RedirectToAction("Cadastro", "Markup");
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