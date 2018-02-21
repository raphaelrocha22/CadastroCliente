using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.WEB.Areas.AreaRestrita.Models.Users;

namespace Projeto.WEB.Areas.AreaRestrita.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        // GET: AreaRestrita/Usuario
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("usuario");

            return RedirectToAction("Login", "Home", new { area = "" });
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Cadastro(int ? id)
        {
            var model = new CadastroViewModel();

            if (id != null)
            {
                var d = new UsuarioDAL();
                Usuario u = d.ObterUsuario((int) id);

                model.IdUsuario = u.IdUsuario;
                model.Nome = u.Nome;
                model.Login = u.Login;
                model.Email = u.Email;
                model.Perfil = u.Perfil;
                model.Status = u.Status;
            }

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Cadastro(CadastroViewModel model)
        {
            try
            {
                if (model.IdUsuario != null)
                {
                    ModelState.Remove("Senha");
                    ModelState.Remove("SenhaConfirm");
                }

                if (ModelState.IsValid)
                {
                    var d = new UsuarioDAL();
                    var u = new Usuario();                                        
                    u.Nome = model.Nome;
                    u.Login = model.Login.ToLower();
                    u.Email = model.Email;
                    u.Perfil = model.Perfil;
                    u.Status = model.Status;
                    
                    if (model.IdUsuario is null)
                    {
                        u.Senha = model.SenhaConfirm;
                        d.CadastrarUsuario(u);
                    }
                    else
                    {
                        u.IdUsuario = (int)model.IdUsuario;
                        d.AtualizarUsuario(u);
                    }                                       
                    
                    TempData["Sucesso"] = true;
                    TempData["Resultado"] = "Usuário cadastrado com sucesso";

                    return RedirectToAction("Cadastro", "Usuario");
                }
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Resultado"] = "Erro: " + e.Message;
            }
            return View(new CadastroViewModel());
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Consulta()
        {
            var lista = new List<ConsultaViewModel>();

            var d = new UsuarioDAL();
            foreach (var item in d.ListarUsuarios())
            {
                var model = new ConsultaViewModel();
                model.IdUsuario = item.IdUsuario;
                model.Nome = item.Nome;
                model.Login = item.Login;
                model.Email = item.Email;
                model.Perfil = item.Perfil;
                model.Status = item.Status;

                lista.Add(model);
            }
            return View(lista);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult AlterarSenha(int id)
        {
            return View(new AlterarSenhaViewModel() { idUsuario = id });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var d = new UsuarioDAL();
                    d.AtualizarSenha(model.SenhaConfirm, model.idUsuario);

                    ModelState.Clear();

                    TempData["Sucesso"] = true;
                    TempData["Resultado"] = "Senha atualizada com sucesso";
                }
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Resultado"] = "Erro: " + e.Message;
            }
            return View();
        }
    }
}