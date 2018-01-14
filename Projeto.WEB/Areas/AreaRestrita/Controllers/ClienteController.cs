using Newtonsoft.Json;
using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.Entidades.Enum;
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
            model.IdSessao = u.IdUsuario;
            return View(model);
        }

        [HttpPost]
        public JsonResult Cadastro(CadastroViewModel model)
        {
            try
            {
                var c = new Cliente();
                c.Enderecos = new List<Endereco>();
                c.Representante = new Representante();

                c.Codun = model.Codun;
                c.RazaoSocial = model.RazaoSocial;
                c.NomeFantasia = model.NomeFantasia;
                c.Cnpj = model.Cnpj;
                c.InscricaoEstadual = model.InscricaoEstadual;
                c.InscricaoMunicipal = model.InscricaoMunicipal;
                c.Classe = model.Classe;
                c.Representante.IdRepresentante = model.IdRepresentante;
                c.Ativo = true;

                c.Enderecos.Add(new Endereco
                {
                    Tipo = model.EnderecoCadastro.Tipo,
                    Logradouro = model.EnderecoCadastro.Logradouro,
                    Numero = model.EnderecoCadastro.Numero,
                    Complemento = model.EnderecoCadastro.Complemento,
                    Bairro = model.EnderecoCadastro.Bairro,
                    Municipio = model.EnderecoCadastro.Municipio,
                    UF = model.EnderecoCadastro.UF,
                    Cep = model.EnderecoCadastro.Cep,
                    Email = model.EnderecoCadastro.Email,
                    Telefone1 = model.EnderecoCadastro.Telefone1,
                    Telefone2 = model.EnderecoCadastro.Telefone2
                });

                c.Enderecos.Add(new Endereco
                {
                    Tipo = model.EnderecoCobranca.Tipo,
                    Logradouro = model.EnderecoCobranca.Logradouro,
                    Numero = model.EnderecoCobranca.Numero,
                    Complemento = model.EnderecoCobranca.Complemento,
                    Bairro = model.EnderecoCobranca.Bairro,
                    Municipio = model.EnderecoCobranca.Municipio,
                    UF = model.EnderecoCobranca.UF,
                    Cep = model.EnderecoCobranca.Cep,
                    Email = model.EnderecoCobranca.Email,
                    Telefone1 = model.EnderecoCobranca.Telefone1,
                    Telefone2 = model.EnderecoCobranca.Telefone2
                });

                c.Enderecos.Add(new Endereco
                {
                    Tipo = model.EnderecoEntrega.Tipo,
                    Logradouro = model.EnderecoEntrega.Logradouro,
                    Numero = model.EnderecoEntrega.Numero,
                    Complemento = model.EnderecoEntrega.Complemento,
                    Bairro = model.EnderecoEntrega.Bairro,
                    Municipio = model.EnderecoEntrega.Municipio,
                    UF = model.EnderecoEntrega.UF,
                    Cep = model.EnderecoEntrega.Cep,
                    Email = model.EnderecoEntrega.Email,
                    Telefone1 = model.EnderecoEntrega.Telefone1,
                    Telefone2 = model.EnderecoEntrega.Telefone2
                });

                var d = new ClienteDAL();
                if (!d.VerificarCNPJ(c.Cnpj))
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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.receitaws.com.br/v1/cnpj/{model.Cnpj}");
                WebResponse response = request.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    var json = JsonConvert.DeserializeObject<JsonCNPJ>(reader.ReadToEnd());

                    model.EnderecoCadastro = new EnderecoViewModel();
                    model.RazaoSocial = json.RazaoSocial;
                    model.NomeFantasia = json.NomeFantasia;
                    model.EnderecoCadastro.Logradouro = json.Logradouro;
                    model.EnderecoCadastro.Numero = json.Numero;
                    model.EnderecoCadastro.Complemento = json.Complemento;
                    model.EnderecoCadastro.Bairro = json.Bairro;
                    model.EnderecoCadastro.Municipio = json.Municipio;
                    model.EnderecoCadastro.UF = json.UF;
                    model.EnderecoCadastro.Cep = json.Cep;
                    model.EnderecoCadastro.Email = json.Email;
                    model.EnderecoCadastro.Telefone1 = json.Telefone1;
                    model.Status = json.Status;
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
            model.IdSessao = u.IdUsuario;
            return View(model);
        }

        [HttpPost]
        public JsonResult Consulta(ConsultaViewModel model)
        {
            try
            {
                var lista = new List<ConsultaViewModel>();

                var d = new ClienteDAL();
                foreach (var c in d.ObterClientes(model.IdCliente, model.CodCliente, model.Codun, model.RazaoSocial, 
                    model.NomeFantasia, model.Cnpj,model.IdRepresentante, model.DataInicio, model.DataFim))
                {
                    model = new ConsultaViewModel();
                    model.EnderecoCadastro = new EnderecoViewModel();
                    model.EnderecoCobranca = new EnderecoViewModel();
                    model.EnderecoEntrega = new EnderecoViewModel();

                    model.IdCliente = c.IdCliente;
                    model.CodCliente = c.CodCliente;
                    model.Codun = c.Codun;
                    model.RazaoSocial = c.RazaoSocial;
                    model.NomeFantasia = c.NomeFantasia;
                    model.Cnpj = c.Cnpj;
                    model.InscricaoEstadual = c.InscricaoEstadual;
                    model.InscricaoMunicipal = c.InscricaoMunicipal;
                    model.Classe = c.Classe.ToString();
                    model.DataCadastro = c.DataCadastro.ToString();
                    model.NomeRepresentante = c.Representante.Nome;

                    foreach (var item in d.ObterEndereco(c.IdCliente))
                    {
                        switch (item.Tipo)
                        {
                            case ("Cadastro"):
                                model.EnderecoCadastro.IdEndereco = item.IdEndereco;
                                model.EnderecoCadastro.Logradouro = item.Logradouro;
                                model.EnderecoCadastro.Numero = item.Numero;
                                model.EnderecoCadastro.Complemento = item.Complemento;
                                model.EnderecoCadastro.Bairro = item.Bairro;
                                model.EnderecoCadastro.Municipio = item.Municipio;
                                model.EnderecoCadastro.UF = item.UF;
                                model.EnderecoCadastro.Cep = item.Cep;
                                model.EnderecoCadastro.Email = item.Email;
                                model.EnderecoCadastro.Telefone1 = item.Telefone1;
                                model.EnderecoCadastro.Telefone2 = item.Telefone2;
                                model.EnderecoCadastro.Tipo = item.Tipo;
                                model.EnderecoCadastro.DataCadastro = item.DataCadastro.ToString("dd/MM/yyyy hh:mm");
                                break;

                            case ("Cobranca"):
                                model.EnderecoCobranca.IdEndereco = item.IdEndereco;
                                model.EnderecoCobranca.Logradouro = item.Logradouro;
                                model.EnderecoCobranca.Numero = item.Numero;
                                model.EnderecoCobranca.Complemento = item.Complemento;
                                model.EnderecoCobranca.Bairro = item.Bairro;
                                model.EnderecoCobranca.Municipio = item.Municipio;
                                model.EnderecoCobranca.UF = item.UF;
                                model.EnderecoCobranca.Cep = item.Cep;
                                model.EnderecoCobranca.Email = item.Email;
                                model.EnderecoCobranca.Telefone1 = item.Telefone1;
                                model.EnderecoCobranca.Telefone2 = item.Telefone2;
                                model.EnderecoCobranca.Tipo = item.Tipo;
                                model.EnderecoCobranca.DataCadastro = item.DataCadastro.ToString("dd/MM/yyyy hh:mm");
                                break;

                            case ("Entrega"):
                                model.EnderecoEntrega.IdEndereco = item.IdEndereco;
                                model.EnderecoEntrega.Logradouro = item.Logradouro;
                                model.EnderecoEntrega.Numero = item.Numero;
                                model.EnderecoEntrega.Complemento = item.Complemento;
                                model.EnderecoEntrega.Bairro = item.Bairro;
                                model.EnderecoEntrega.Municipio = item.Municipio;
                                model.EnderecoEntrega.UF = item.UF;
                                model.EnderecoEntrega.Cep = item.Cep;
                                model.EnderecoEntrega.Email = item.Email;
                                model.EnderecoEntrega.Telefone1 = item.Telefone1;
                                model.EnderecoEntrega.Telefone2 = item.Telefone2;
                                model.EnderecoEntrega.Tipo = item.Tipo;
                                model.EnderecoEntrega.DataCadastro = item.DataCadastro.ToString("dd/MM/yyyy hh:mm");
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
                model.EnderecoCadastro = new EnderecoViewModel();
                model.EnderecoCobranca = new EnderecoViewModel();
                model.EnderecoEntrega = new EnderecoViewModel();

                var d = new ClienteDAL();
                foreach (var c in d.ObterClientes(id))
                {
                    model.IdCliente = c.IdCliente;
                    model.CodCliente = c.CodCliente;
                    model.Codun = c.Codun;
                    model.RazaoSocial = c.RazaoSocial;
                    model.NomeFantasia = c.NomeFantasia;
                    model.Cnpj = c.Cnpj;
                    model.InscricaoEstadual = c.InscricaoEstadual;
                    model.InscricaoMunicipal = c.InscricaoMunicipal;
                    model.Classe = c.Classe;
                    model.IdRepresentante = c.Representante.IdRepresentante;
                    model.IdSessao = u.IdUsuario;

                    foreach (var item in d.ObterEndereco(c.IdCliente))
                    {
                        switch (item.Tipo)
                        {
                            case ("Cadastro"):
                                model.EnderecoCadastro.IdEndereco = item.IdEndereco;
                                model.EnderecoCadastro.Logradouro = item.Logradouro;
                                model.EnderecoCadastro.Numero = item.Numero;
                                model.EnderecoCadastro.Complemento = item.Complemento;
                                model.EnderecoCadastro.Bairro = item.Bairro;
                                model.EnderecoCadastro.Municipio = item.Municipio;
                                model.EnderecoCadastro.UF = item.UF;
                                model.EnderecoCadastro.Cep = item.Cep;
                                model.EnderecoCadastro.Email = item.Email;
                                model.EnderecoCadastro.Telefone1 = item.Telefone1;
                                model.EnderecoCadastro.Telefone2 = item.Telefone2;
                                model.EnderecoCadastro.Tipo = item.Tipo;
                                break;

                            case ("Cobranca"):
                                model.EnderecoCobranca.IdEndereco = item.IdEndereco;
                                model.EnderecoCobranca.Logradouro = item.Logradouro;
                                model.EnderecoCobranca.Numero = item.Numero;
                                model.EnderecoCobranca.Complemento = item.Complemento;
                                model.EnderecoCobranca.Bairro = item.Bairro;
                                model.EnderecoCobranca.Municipio = item.Municipio;
                                model.EnderecoCobranca.UF = item.UF;
                                model.EnderecoCobranca.Cep = item.Cep;
                                model.EnderecoCobranca.Email = item.Email;
                                model.EnderecoCobranca.Telefone1 = item.Telefone1;
                                model.EnderecoCobranca.Telefone2 = item.Telefone2;
                                model.EnderecoCobranca.Tipo = item.Tipo;
                                break;

                            case ("Entrega"):
                                model.EnderecoEntrega.IdEndereco = item.IdEndereco;
                                model.EnderecoEntrega.Logradouro = item.Logradouro;
                                model.EnderecoEntrega.Numero = item.Numero;
                                model.EnderecoEntrega.Complemento = item.Complemento;
                                model.EnderecoEntrega.Bairro = item.Bairro;
                                model.EnderecoEntrega.Municipio = item.Municipio;
                                model.EnderecoEntrega.UF = item.UF;
                                model.EnderecoEntrega.Cep = item.Cep;
                                model.EnderecoEntrega.Email = item.Email;
                                model.EnderecoEntrega.Telefone1 = item.Telefone1;
                                model.EnderecoEntrega.Telefone2 = item.Telefone2;
                                model.EnderecoEntrega.Tipo = item.Tipo;
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
                c.Enderecos = new List<Endereco>();

                c.Representante = new Representante { IdRepresentante = model.IdRepresentante };
                c.IdCliente = model.IdCliente;
                c.Codun = model.Codun;
                c.RazaoSocial = model.RazaoSocial;
                c.CodCliente = model.CodCliente;
                c.NomeFantasia = model.NomeFantasia;
                c.Cnpj = model.Cnpj;
                c.InscricaoEstadual = model.InscricaoEstadual;
                c.InscricaoMunicipal = model.InscricaoMunicipal;
                c.Classe = model.Classe;
                c.Ativo = true;
                c.Enderecos.Add(new Endereco
                {
                    IdEndereco = model.EnderecoCadastro.IdEndereco,
                    Tipo = model.EnderecoCadastro.Tipo,
                    Logradouro = model.EnderecoCadastro.Logradouro,
                    Numero = model.EnderecoCadastro.Numero,
                    Complemento = model.EnderecoCadastro.Complemento,
                    Bairro = model.EnderecoCadastro.Bairro,
                    Municipio = model.EnderecoCadastro.Municipio,
                    UF = model.EnderecoCadastro.UF,
                    Cep = model.EnderecoCadastro.Cep,
                    Email = model.EnderecoCadastro.Email,
                    Telefone1 = model.EnderecoCadastro.Telefone1,
                    Telefone2 = model.EnderecoCadastro.Telefone2
                });
                c.Enderecos.Add(new Endereco
                {
                    IdEndereco = model.EnderecoCobranca.IdEndereco,
                    Tipo = model.EnderecoCobranca.Tipo,
                    Logradouro = model.EnderecoCobranca.Logradouro,
                    Numero = model.EnderecoCobranca.Numero,
                    Complemento = model.EnderecoCobranca.Complemento,
                    Bairro = model.EnderecoCobranca.Bairro,
                    Municipio = model.EnderecoCobranca.Municipio,
                    UF = model.EnderecoCobranca.UF,
                    Cep = model.EnderecoCobranca.Cep,
                    Email = model.EnderecoCobranca.Email,
                    Telefone1 = model.EnderecoCobranca.Telefone1,
                    Telefone2 = model.EnderecoCobranca.Telefone2
                });
                c.Enderecos.Add(new Endereco
                {
                    IdEndereco = model.EnderecoEntrega.IdEndereco,
                    Tipo = model.EnderecoEntrega.Tipo,
                    Logradouro = model.EnderecoEntrega.Logradouro,
                    Numero = model.EnderecoEntrega.Numero,
                    Complemento = model.EnderecoEntrega.Complemento,
                    Bairro = model.EnderecoEntrega.Bairro,
                    Municipio = model.EnderecoEntrega.Municipio,
                    UF = model.EnderecoEntrega.UF,
                    Cep = model.EnderecoEntrega.Cep,
                    Email = model.EnderecoEntrega.Email,
                    Telefone1 = model.EnderecoEntrega.Telefone1,
                    Telefone2 = model.EnderecoEntrega.Telefone2
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