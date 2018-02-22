using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.Entidades.Enuns;
using Projeto.Util;
using Projeto.WEB.Areas.AreaRestrita.Models.SemCampanha;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Controllers
{
    [Authorize]
    public class SemCampanhaController : Controller
    {
        // GET: AreaRestrita/SemCampanha
        public ActionResult Cadastro(int ? id)
        {
            var model = new CadastroViewModel();

            if (id != null)
                model.IdTransacao = (int)id;

            return View(model);
        }

        public JsonResult Versao(CadastroViewModel model)
        {
            var d = new SemCampanhaDAL();
            int versao = d.Versao(model.Codun, model.CodCliente, model.IdTransacao);
            return Json(versao);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Cadastro(CadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var c = new SemCampanha();
                    c.Cliente = new Cliente();
                    c.Usuario = new Usuario();

                    c.Campanha = Campanha.SemCampanha;
                    c.Cliente.IdCliente = model.IdTransacao;
                    c.Cliente.CodCliente = model.CodCliente;
                    c.Cliente.Codun = model.Codun;                    
                    c.Versao = model.Versao;
                    c.DataNegociacao = model.DataNegociacao != null ? Convert.ToDateTime(model.DataNegociacao) : (DateTime)SqlDateTime.MinValue;
                    c.DataInicio = Convert.ToDateTime(model.DataInicioContrato);
                    c.Markup = model.MarkUP;
                    c.Desconto = 1 - (2.52M / c.Markup);
                    c.MesesPagamentoRBR = model.MesesPagamentoRBR;
                    c.NetlineHabilitado = model.MesesPagamentoNetline != 0;
                    c.MesesPagamentoNetline = model.MesesPagamentoNetline;
                    c.Guelta = model.Guelta;
                    c.Status = StatusSolicitacao.Pendente;
                    c.Observacao = model.Obervacao;
                    c.Acao = Acao.Cadastrar;
                    c.Usuario = model.usuario;

                    var d = new SemCampanhaDAL();
                    d.Cadastrar(c);

                    var r = new RepresentanteDAL();
                    List<string> destinatarios = r.ListaDestinatarios(c.Usuario.IdUsuario);

                    Email.EnviarEmailSemCampanha(c, destinatarios);

                    TempData["Sucesso"] = true;
                    TempData["Resultado"] = "Solicitação de Cadastro enviada com sucesso. \n" +
                        "Um E-mail de confirmação foi enviado, assim que o cliente estiver cadastrado você receberá uma confirmação via E-mail";
                    return RedirectToAction("Cadastro", "SemCampanha");
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