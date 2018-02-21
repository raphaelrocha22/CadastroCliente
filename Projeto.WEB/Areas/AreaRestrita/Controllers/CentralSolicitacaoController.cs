using Projeto.DAL.Persistencia;
using Projeto.Entidades.Enuns;
using Projeto.WEB.Areas.AreaRestrita.Models.CentralSolicitacao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Controllers
{
    public class CentralSolicitacaoController : Controller
    {
        // GET: AreaRestrita/CentralSolicitacao
        public ActionResult Index()
        {
            try
            {
                var lista = new List<ConsultaViewModel>();

                var d = new CentralSolicitacaoDAL();
                foreach (var item in d.Consultar(StatusSolicitacao.Pendente))
                {
                    var m = new ConsultaViewModel();
                    m.Id = item.Id;
                    m.Tabela = item.Tabela;
                    m.Campanha = item.Campanha != 0 ? item.Campanha.ToString() : "-";
                    m.CodCliente = item.CodCliente;
                    m.Codun = item.Codun;
                    m.Acao = item.Acao;
                    m.Usuario = item.NomeUsuario;

                    lista.Add(m);
                }
                return View(lista);
            }
            catch (Exception e)
            {
                ViewBag.Mensagem = e.Message;
                return View();
            }
        }
    }
}