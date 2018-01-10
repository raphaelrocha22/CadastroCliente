using Newtonsoft.Json;
using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.WEB.Areas.AreaRestrita.Models.Cliente;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

        [HttpPost]
        public JsonResult Cadastro(CadastroViewModel model)
        {
            try
            {
                model.cliente.enderecos = new List<Endereco>();
                model.cliente.enderecos.Add(model.cliente.enderecoCadastro);
                model.cliente.enderecos.Add(model.cliente.enderecoCobranca);
                model.cliente.enderecos.Add(model.cliente.enderecoEntrega);

                var d = new ClienteDAL();
                d.CadastrarCliente(model.cliente);

                return Json("Usuario cadastrado com sucesso");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }            
        }

        public JsonResult ConsultarCNPJ(CadastroViewModel model)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.receitaws.com.br/v1/cnpj/{model.Cnpj}");
                WebResponse response = request.GetResponse();
                
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    model = JsonConvert.DeserializeObject<CadastroViewModel>(reader.ReadToEnd());
                }              

                return Json(model);

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}