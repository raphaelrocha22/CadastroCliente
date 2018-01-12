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
        public JsonResult Cadastro(ClienteViewModel model)
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
                c.ativo = true;
                c.endereco.Add(model.enderecoCadastro);
                c.endereco.Add(model.enderecoCobranca);
                c.endereco.Add(model.enderecoEntrega);
                c.representante = model.representante;

                var d = new ClienteDAL();
                if (!d.VerificarCNPJ(c.cnpj))
                {
                    d.CadastrarCliente(c);
                    return Json("Usuario cadastrado com sucesso");
                }
                else
                {
                    return Json("Já existe um cliente cadastrado com o CNPJ informado");
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public JsonResult ConsultarCNPJ(ClienteViewModel model)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.receitaws.com.br/v1/cnpj/{model.cnpj}");
                WebResponse response = request.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    model = JsonConvert.DeserializeObject<ClienteViewModel>(reader.ReadToEnd());
                }

                return Json(model);

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public ActionResult Consulta()
        {
            //carregar o dropBox representantes
            return View();
        }

        [HttpPost]
        public JsonResult Consulta(ClienteViewModel model)
        {
            try
            {
                var lista = new List<ClienteViewModel>();

                var d = new ClienteDAL();
                foreach (var c in d.ObterClientes(model.codCliente, model.codun, model.razaoSocial, 
                    model.nomeFantasia, model.cnpj,model.representante.idRepresentante, model.dataInicio, model.dataFim))
                {
                    model.idCliente = c.idCliente;
                    model.codCliente = c.codCliente;
                    model.codun = c.codun;
                    model.razaoSocial = c.razaoSocial;
                    model.nomeFantasia = c.nomeFantasia;
                    model.cnpj = c.cnpj;
                    model.inscricaoEstadual = c.inscricaoEstadual;
                    model.inscricaoMunicipal = c.inscricaoMunicipal;
                    model.classe = c.classe;
                    model.representante.idRepresentante = c.representante.idRepresentante;
                    model.representante.nome = c.representante.nome;

                    foreach (var item in d.ObterEndereco(c.idCliente))
                    {
                        switch (item.tipo)
                        {
                            case ("Cadastro"):
                                model.enderecoCadastro = item;
                                break;

                            case ("Cobranca"):
                                model.enderecoCobranca = item;
                                break;

                            case ("Entrega"):
                                model.enderecoEntrega = item;
                                break;
                        }
                    }

                    lista.Add(model);
                }
                return Json(lista);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public JsonResult ObterCliente(int id)
        {
            try
            {
                var model = new ClienteViewModel();
                model.representante = new Representante();
                model.enderecoCadastro = new Endereco();
                model.enderecoCobranca = new Endereco();
                model.enderecoEntrega = new Endereco();

                var d = new ClienteDAL();
                Cliente c = d.ObterCliente(id);

                model.idCliente = c.idCliente;
                model.codCliente = c.codCliente;
                model.codun = c.codun;
                model.razaoSocial = c.razaoSocial;
                model.nomeFantasia = c.nomeFantasia;
                model.cnpj = c.cnpj;
                model.inscricaoEstadual = c.inscricaoEstadual;
                model.inscricaoMunicipal = c.inscricaoMunicipal;
                model.classe = c.classe;
                model.representante.idRepresentante = c.representante.idRepresentante;
                model.representante.nome = c.representante.nome;

                foreach (var item in d.ObterEndereco(c.idCliente))
                {
                    switch (item.tipo)
                    {
                        case ("Cadastro"):
                            model.enderecoCadastro = item;
                            break;

                        case ("Cobranca"):
                            model.enderecoCobranca = item;
                            break;

                        case ("Entrega"):
                            model.enderecoEntrega = item;
                            break;
                    }
                }
                return Json(model);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }
    }
}