using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.WEB.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Projeto.WEB.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var d = new UsuarioDAL();
                    Usuario u = d.ObterUsuarioSenha(model.login, model.senha);

                    if (u != null)
                    {
                        var ticket = new FormsAuthenticationTicket(u.login, false, 60);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                        Response.Cookies.Add(cookie);

                        Session.Add("usuario", u);

                        return RedirectToAction("Index", "Cliente", new { area = "AreaRestrita" });
                    }
                    else
                    {
                        ViewBag.Mensagem = "Acesso negado, usuário ou senha incorretos";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = $"Erro não esperado, por favor entre em contato com o administrador do sistema. Erro: {e.Message}";
                }
            }
            return View();
        }

        public ActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlterarSenha(UpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var d = new UsuarioDAL();
                    Usuario u = d.ObterUsuarioSenha(model.login, model.senhaAntiga);

                    if (u != null)
                    {
                        d.AtualizarSenha(model.senha, u.idUsuario);

                        ViewBag.Resultado = true;
                        ViewBag.Mensagem = "Senha alterada com sucesso";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Resultado = false;
                        ViewBag.Mensagem = "Não foi possível completar a operação, usuário ou senha incorretos";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Resultado = false;
                    ViewBag.Mensagem = $"Erro não esperado, por favor entre em contato com o administrador do sistema. Erro: {e.Message}";
                }
            }
            return View();
        }
    }
}