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
            var lista = new List<CadastroViewModel>();

            lista.Add(new CadastroViewModel {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Ouro"),
                PrazoPagamento = "trimestral"
            });
            lista.Add(new CadastroViewModel
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Diamante"),
                PrazoPagamento = "Semestral"
            });
            lista.Add(new CadastroViewModel
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Diamante"),
                PrazoPagamento = "Anual"
            });
            lista.Add(new CadastroViewModel
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Platina"),
                PrazoPagamento = "Anual"
            });

            var busca = lista.Where(c => c.PrazoPagamento.Equals("Anual"));

            var result = busca.ToList();

            return View();
        }
        
        [HttpPost]
        public JsonResult Cadastro(CadastroViewModel mode)
        {
            return Json("mensagem");
        }
    }
}