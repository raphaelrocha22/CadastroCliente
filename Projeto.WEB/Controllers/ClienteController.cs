using Newtonsoft.Json;
using Projeto.WEB.Models;
using Projeto.WEB.Models.JsonClass;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;



namespace Projeto.WEB.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
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