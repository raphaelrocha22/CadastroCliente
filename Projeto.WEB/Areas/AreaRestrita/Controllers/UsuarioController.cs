using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Projeto.WEB.Areas.AreaRestrita.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: AreaRestrita/Usuario
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("usuario");

            return RedirectToAction("Login", "Home", new { area = "" });
        }
    }
}