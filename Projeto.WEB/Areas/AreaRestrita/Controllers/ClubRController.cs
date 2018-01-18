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
        
        [HttpPost]
        public JsonResult Cadastro(CadastroViewModel mode)
        {
            return Json("mensagem");
        }
    }
}