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
                var c = new Cliente();
                c.endereco = new List<Endereco>();
                c.representante = new Representante();

                c.codun = model.codun;
                c.razaoSocial = model.razaoSocial;
                c.nomeFantasia = model.nomeFantasia;
                c.cnpj = model.cnpj;
                c.inscricaoEstadual = model.inscricaoEstadual;
                c.inscricaoMunicipal = model.inscricaoMunicipal;
                c.classe = model.classe;
                c.endereco.Add(model.enderecoCadastro);
                c.endereco.Add(model.enderecoCobranca);
                c.endereco.Add(model.enderecoEntrega);
                c.representante = model.representante;

                var d = new ClienteDAL();
                d.CadastrarCliente(c);

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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.receitaws.com.br/v1/cnpj/{model.cnpj}");
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