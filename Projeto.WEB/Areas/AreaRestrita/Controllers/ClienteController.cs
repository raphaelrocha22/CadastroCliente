using Newtonsoft.Json;
using Projeto.WEB.Areas.AreaRestrita.Models.Cliente;
using Projeto.WEB.Areas.AreaRestrita.Models.JsonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        // GET: AreaRestrita/Cliente
        public ActionResult Index()
        {
            return View();
        }
                
        public ActionResult Cadastro()
        {
            return View();
        }

        public JsonResult ConsultarCNPJ(ClienteViewModel model)
        {
            try
            {
                var json = JSONHelper.GetJSONString(model.NumeroCNPJ);
                model = JsonConvert.DeserializeObject<ClienteViewModel>(json);

                return Json(model);

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}