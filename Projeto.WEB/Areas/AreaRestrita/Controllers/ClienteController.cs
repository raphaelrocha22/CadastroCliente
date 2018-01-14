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
            Usuario u = (Usuario)Session["usuario"];

            var model = new CadastroViewModel();
            model.idSessao = u.idUsuario;
            return View(model);
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
                c.representante.idRepresentante = model.idRepresentante;
                c.ativo = true;

                c.endereco.Add(new Endereco
                {
                    tipo = model.enderecoCadastro.tipo,
                    logradouro = model.enderecoCadastro.logradouro,
                    numero = model.enderecoCadastro.numero,
                    complemento = model.enderecoCadastro.complemento,
                    bairro = model.enderecoCadastro.bairro,
                    municipio = model.enderecoCadastro.municipio,
                    UF = model.enderecoCadastro.uf,
                    cep = model.enderecoCadastro.cep,
                    email = model.enderecoCadastro.email,
                    telefone1 = model.enderecoCadastro.telefone1,
                    telefone2 = model.enderecoCadastro.telefone2
                });

                c.endereco.Add(new Endereco
                {
                    tipo = model.enderecoCobranca.tipo,
                    logradouro = model.enderecoCobranca.logradouro,
                    numero = model.enderecoCobranca.numero,
                    complemento = model.enderecoCobranca.complemento,
                    bairro = model.enderecoCobranca.bairro,
                    municipio = model.enderecoCobranca.municipio,
                    UF = model.enderecoCobranca.uf,
                    cep = model.enderecoCobranca.cep,
                    email = model.enderecoCobranca.email,
                    telefone1 = model.enderecoCobranca.telefone1,
                    telefone2 = model.enderecoCobranca.telefone2
                });

                c.endereco.Add(new Endereco
                {
                    tipo = model.enderecoEntrega.tipo,
                    logradouro = model.enderecoEntrega.logradouro,
                    numero = model.enderecoEntrega.numero,
                    complemento = model.enderecoEntrega.complemento,
                    bairro = model.enderecoEntrega.bairro,
                    municipio = model.enderecoEntrega.municipio,
                    UF = model.enderecoEntrega.uf,
                    cep = model.enderecoEntrega.cep,
                    email = model.enderecoEntrega.email,
                    telefone1 = model.enderecoEntrega.telefone1,
                    telefone2 = model.enderecoEntrega.telefone2
                });

                var d = new ClienteDAL();
                if (!d.VerificarCNPJ(c.cnpj))
                {
                    d.CadastrarCliente(c);
                    return Json("Cliente cadastrado com sucesso");
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

        public JsonResult ConsultarCNPJ(CadastroViewModel model)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.receitaws.com.br/v1/cnpj/{model.cnpj}");
                WebResponse response = request.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    var json = JsonConvert.DeserializeObject<JsonCNPJ>(reader.ReadToEnd());

                    model.enderecoCadastro = new EnderecoViewModel();
                    model.razaoSocial = json.razaoSocial;
                    model.nomeFantasia = json.nomeFantasia;
                    model.enderecoCadastro.logradouro = json.logradouro;
                    model.enderecoCadastro.numero = json.numero;
                    model.enderecoCadastro.complemento = json.complemento;
                    model.enderecoCadastro.bairro = json.bairro;
                    model.enderecoCadastro.municipio = json.municipio;
                    model.enderecoCadastro.uf = json.uf;
                    model.enderecoCadastro.cep = json.cep;
                    model.enderecoCadastro.email = json.email;
                    model.enderecoCadastro.telefone1 = json.telefone1;
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
            Usuario u = (Usuario)Session["usuario"];

            var model = new ConsultaViewModel();
            model.idSessao = u.idUsuario;
            return View(model);
        }

        [HttpPost]
        public JsonResult Consulta(ConsultaViewModel model)
        {
            try
            {
                var lista = new List<ConsultaViewModel>();

                var d = new ClienteDAL();
                foreach (var c in d.ObterClientes(model.idCliente, model.codCliente, model.codun, model.razaoSocial, 
                    model.nomeFantasia, model.cnpj,model.idRepresentante, model.dataInicio, model.dataFim))
                {
                    model = new ConsultaViewModel();
                    model.enderecoCadastro = new EnderecoViewModel();
                    model.enderecoCobranca = new EnderecoViewModel();
                    model.enderecoEntrega = new EnderecoViewModel();

                    model.idCliente = c.idCliente;
                    model.codCliente = c.codCliente;
                    model.codun = c.codun;
                    model.razaoSocial = c.razaoSocial;
                    model.nomeFantasia = c.nomeFantasia;
                    model.cnpj = c.cnpj;
                    model.inscricaoEstadual = c.inscricaoEstadual;
                    model.inscricaoMunicipal = c.inscricaoMunicipal;
                    model.classe = c.classe;
                    model.dataCadastro = c.dataCadastro.ToString();
                    model.nomeRepresentante = c.representante.nome;

                    foreach (var item in d.ObterEndereco(c.idCliente))
                    {
                        switch (item.tipo)
                        {
                            case ("Cadastro"):
                                model.enderecoCadastro.idEndereco = item.idEndereco;
                                model.enderecoCadastro.logradouro = item.logradouro;
                                model.enderecoCadastro.numero = item.numero;
                                model.enderecoCadastro.complemento = item.complemento;
                                model.enderecoCadastro.bairro = item.bairro;
                                model.enderecoCadastro.municipio = item.municipio;
                                model.enderecoCadastro.uf = item.UF;
                                model.enderecoCadastro.cep = item.cep;
                                model.enderecoCadastro.email = item.email;
                                model.enderecoCadastro.telefone1 = item.telefone1;
                                model.enderecoCadastro.telefone2 = item.telefone2;
                                model.enderecoCadastro.tipo = item.tipo;
                                model.enderecoCadastro.dataCadastro = item.dataCadastro.ToString("dd/MM/yyyy hh:mm");
                                break;

                            case ("Cobranca"):
                                model.enderecoCobranca.idEndereco = item.idEndereco;
                                model.enderecoCobranca.logradouro = item.logradouro;
                                model.enderecoCobranca.numero = item.numero;
                                model.enderecoCobranca.complemento = item.complemento;
                                model.enderecoCobranca.bairro = item.bairro;
                                model.enderecoCobranca.municipio = item.municipio;
                                model.enderecoCobranca.uf = item.UF;
                                model.enderecoCobranca.cep = item.cep;
                                model.enderecoCobranca.email = item.email;
                                model.enderecoCobranca.telefone1 = item.telefone1;
                                model.enderecoCobranca.telefone2 = item.telefone2;
                                model.enderecoCobranca.tipo = item.tipo;
                                model.enderecoCobranca.dataCadastro = item.dataCadastro.ToString("dd/MM/yyyy hh:mm");
                                break;

                            case ("Entrega"):
                                model.enderecoEntrega.idEndereco = item.idEndereco;
                                model.enderecoEntrega.logradouro = item.logradouro;
                                model.enderecoEntrega.numero = item.numero;
                                model.enderecoEntrega.complemento = item.complemento;
                                model.enderecoEntrega.bairro = item.bairro;
                                model.enderecoEntrega.municipio = item.municipio;
                                model.enderecoEntrega.uf = item.UF;
                                model.enderecoEntrega.cep = item.cep;
                                model.enderecoEntrega.email = item.email;
                                model.enderecoEntrega.telefone1 = item.telefone1;
                                model.enderecoEntrega.telefone2 = item.telefone2;
                                model.enderecoEntrega.tipo = item.tipo;
                                model.enderecoEntrega.dataCadastro = item.dataCadastro.ToString("dd/MM/yyyy hh:mm");
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

        public ActionResult Edicao(int id)
        {
            try
            {
                Usuario u = (Usuario)Session["usuario"];

                var model = new EdicaoViewModel();
                model.enderecoCadastro = new EnderecoViewModel();
                model.enderecoCobranca = new EnderecoViewModel();
                model.enderecoEntrega = new EnderecoViewModel();

                var d = new ClienteDAL();
                foreach (var c in d.ObterClientes(id))
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
                    model.idRepresentante = c.representante.idRepresentante;
                    model.idSessao = u.idUsuario;

                    foreach (var item in d.ObterEndereco(c.idCliente))
                    {
                        switch (item.tipo)
                        {
                            case ("Cadastro"):
                                model.enderecoCadastro.idEndereco = item.idEndereco;
                                model.enderecoCadastro.logradouro = item.logradouro;
                                model.enderecoCadastro.numero = item.numero;
                                model.enderecoCadastro.complemento = item.complemento;
                                model.enderecoCadastro.bairro = item.bairro;
                                model.enderecoCadastro.municipio = item.municipio;
                                model.enderecoCadastro.uf = item.UF;
                                model.enderecoCadastro.cep = item.cep;
                                model.enderecoCadastro.email = item.email;
                                model.enderecoCadastro.telefone1 = item.telefone1;
                                model.enderecoCadastro.telefone2 = item.telefone2;
                                model.enderecoCadastro.tipo = item.tipo;
                                break;

                            case ("Cobranca"):
                                model.enderecoCobranca.idEndereco = item.idEndereco;
                                model.enderecoCobranca.logradouro = item.logradouro;
                                model.enderecoCobranca.numero = item.numero;
                                model.enderecoCobranca.complemento = item.complemento;
                                model.enderecoCobranca.bairro = item.bairro;
                                model.enderecoCobranca.municipio = item.municipio;
                                model.enderecoCobranca.uf = item.UF;
                                model.enderecoCobranca.cep = item.cep;
                                model.enderecoCobranca.email = item.email;
                                model.enderecoCobranca.telefone1 = item.telefone1;
                                model.enderecoCobranca.telefone2 = item.telefone2;
                                model.enderecoCobranca.tipo = item.tipo;
                                break;

                            case ("Entrega"):
                                model.enderecoEntrega.idEndereco = item.idEndereco;
                                model.enderecoEntrega.logradouro = item.logradouro;
                                model.enderecoEntrega.numero = item.numero;
                                model.enderecoEntrega.complemento = item.complemento;
                                model.enderecoEntrega.bairro = item.bairro;
                                model.enderecoEntrega.municipio = item.municipio;
                                model.enderecoEntrega.uf = item.UF;
                                model.enderecoEntrega.cep = item.cep;
                                model.enderecoEntrega.email = item.email;
                                model.enderecoEntrega.telefone1 = item.telefone1;
                                model.enderecoEntrega.telefone2 = item.telefone2;
                                model.enderecoEntrega.tipo = item.tipo;
                                break;
                        }
                    }
                }
                return View(model);
            }
            catch (Exception e)
            {
                return ViewBag.Mensagem = e.Message;
            }
        }

        [HttpPost]
        public JsonResult Edicao(EdicaoViewModel model)
        {
            try
            {
                var c = new Cliente();
                c.endereco = new List<Endereco>();

                c.representante = new Representante { idRepresentante = model.idRepresentante };
                c.idCliente = model.idCliente;
                c.codun = model.codun;
                c.razaoSocial = model.razaoSocial;
                c.nomeFantasia = model.nomeFantasia;
                c.cnpj = model.cnpj;
                c.inscricaoEstadual = model.inscricaoEstadual;
                c.inscricaoMunicipal = model.inscricaoMunicipal;
                c.classe = model.classe;
                c.ativo = true;
                c.endereco.Add(new Endereco
                {
                    idEndereco = model.enderecoCadastro.idEndereco,
                    tipo = model.enderecoCadastro.tipo,
                    logradouro = model.enderecoCadastro.logradouro,
                    numero = model.enderecoCadastro.numero,
                    complemento = model.enderecoCadastro.complemento,
                    bairro = model.enderecoCadastro.bairro,
                    municipio = model.enderecoCadastro.municipio,
                    UF = model.enderecoCadastro.uf,
                    cep = model.enderecoCadastro.cep,
                    email = model.enderecoCadastro.email,
                    telefone1 = model.enderecoCadastro.telefone1,
                    telefone2 = model.enderecoCadastro.telefone2
                });
                c.endereco.Add(new Endereco
                {
                    idEndereco = model.enderecoCobranca.idEndereco,
                    tipo = model.enderecoCobranca.tipo,
                    logradouro = model.enderecoCobranca.logradouro,
                    numero = model.enderecoCobranca.numero,
                    complemento = model.enderecoCobranca.complemento,
                    bairro = model.enderecoCobranca.bairro,
                    municipio = model.enderecoCobranca.municipio,
                    UF = model.enderecoCobranca.uf,
                    cep = model.enderecoCobranca.cep,
                    email = model.enderecoCobranca.email,
                    telefone1 = model.enderecoCobranca.telefone1,
                    telefone2 = model.enderecoCobranca.telefone2
                });
                c.endereco.Add(new Endereco
                {
                    idEndereco = model.enderecoEntrega.idEndereco,
                    tipo = model.enderecoEntrega.tipo,
                    logradouro = model.enderecoEntrega.logradouro,
                    numero = model.enderecoEntrega.numero,
                    complemento = model.enderecoEntrega.complemento,
                    bairro = model.enderecoEntrega.bairro,
                    municipio = model.enderecoEntrega.municipio,
                    UF = model.enderecoEntrega.uf,
                    cep = model.enderecoEntrega.cep,
                    email = model.enderecoEntrega.email,
                    telefone1 = model.enderecoEntrega.telefone1,
                    telefone2 = model.enderecoEntrega.telefone2
                });
                
                var d = new ClienteDAL();
                d.AtualizarCliente(c);

                return Json("Cliente atualizado com sucesso");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}