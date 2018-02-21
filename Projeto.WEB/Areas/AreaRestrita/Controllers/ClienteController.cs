using Newtonsoft.Json;
using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.Entidades.Enuns;
using Projeto.Util;
using Projeto.WEB.Areas.AreaRestrita.Models.Cliente;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

        public ActionResult Cadastro(int? id)
        {
            try
            {
                var model = new CadastroViewModel();
                model.EnderecoCadastro = new EnderecoViewModel();
                model.EnderecoCobranca = new EnderecoViewModel();
                model.EnderecoEntrega = new EnderecoViewModel();

                //Se id not null -> Edicao
                if (id != null)
                {
                    var d = new ClienteDAL();
                    foreach (var c in d.ObterClientes((int)id))
                    {
                        model.IdTransacao = c.IdCliente;
                        model.CodCliente = c.CodCliente;
                        model.Codun = c.Codun;
                        model.RazaoSocial = c.RazaoSocial;
                        model.NomeFantasia = c.NomeFantasia;
                        model.Cnpj = c.Cnpj;
                        model.InscricaoEstadual = c.InscricaoEstadual;
                        model.InscricaoMunicipal = c.InscricaoMunicipal;
                        model.Classe = c.Classe;
                        model.Observacao = c.Observacao;
                        model.IdAgente = c.Agente.IdRepresentante;
                        model.IdPromotor = c.Promotor.IdRepresentante;

                        foreach (var item in d.ObterEndereco(c.IdCliente))
                        {
                            switch (item.Tipo)
                            {
                                case (TipoEndereco.Cadastro):
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

                                case (TipoEndereco.Cobranca):
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
                                    model.CobrancaIgualCadastro = item.IgualCadastro;
                                    break;

                                case (TipoEndereco.Entrega):
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
                                    model.EntregaIgualCadastro = item.IgualCadastro;
                                    model.EntregaIgualCobranca = item.IgualCobranca;
                                    break;
                            }
                        }
                    }
                }
                return View(model);
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = true;
                TempData["Resultado"] = e.Message;

                return View(new CadastroViewModel());
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Cadastro(CadastroViewModel model)
        {
            try
            {
                var keyList = new List<string>();

                if (model.CobrancaIgualCadastro)
                {
                    keyList.Add("EnderecoCobranca");
                    model.EnderecoCobranca = new EnderecoViewModel(model.EnderecoCadastro);
                }

                if (model.EntregaIgualCadastro)
                {
                    keyList.Add("EnderecoEntrega");
                    model.EnderecoEntrega = new EnderecoViewModel(model.EnderecoCadastro);
                }
                else if (model.EntregaIgualCobranca)
                {
                    keyList.Add("EnderecoEntrega");
                    model.EnderecoEntrega = new EnderecoViewModel(model.EnderecoCobranca);
                }

                foreach (var item in keyList)
                {
                    foreach (var key in ModelState.Keys.ToList().Where(key => key.StartsWith((item))))
                    {
                        ModelState.Remove(key);
                    }
                }

                if (ModelState.IsValid)
                {
                    var c = new Cliente();
                    c.Agente = new Representante();
                    c.Promotor = new Representante();
                    c.Usuario = new Usuario();

                    c.CodCliente = model.CodCliente;
                    c.Codun = model.Codun;
                    c.RazaoSocial = model.RazaoSocial;
                    c.NomeFantasia = model.NomeFantasia;
                    c.Cnpj = model.Cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                    c.InscricaoEstadual = model.InscricaoEstadual;
                    c.InscricaoMunicipal = model.InscricaoMunicipal;
                    c.Classe = model.Classe;
                    c.Status = StatusSolicitacao.Pendente;
                    c.Observacao = model.Observacao;
                    c.DataCadastro = DateTime.Now;
                    c.Agente.IdRepresentante = model.IdAgente;
                    c.Promotor.IdRepresentante = model.IdPromotor;
                    c.Usuario = model.usuario;

                    //Necessario passar o idEndereco para caso de Edicao
                    c.Enderecos = new List<Endereco>()
                    {
                        new Endereco(model.EnderecoCadastro.IdEndereco,TipoEndereco.Cadastro, model.EnderecoCadastro.Logradouro, model.EnderecoCadastro.Numero,
                        model.EnderecoCadastro.Complemento,model.EnderecoCadastro.Bairro,model.EnderecoCadastro.Municipio,model.EnderecoCadastro.UF,
                        model.EnderecoCadastro.Cep,model.EnderecoCadastro.Telefone1,model.EnderecoCadastro.Telefone2,model.EnderecoCadastro.Email,false,false),

                        new Endereco(model.EnderecoCobranca.IdEndereco,TipoEndereco.Cobranca,model.EnderecoCobranca.Logradouro,model.EnderecoCobranca.Numero,
                        model.EnderecoCobranca.Complemento,model.EnderecoCobranca.Bairro,model.EnderecoCobranca.Municipio,model.EnderecoCobranca.UF,
                        model.EnderecoCobranca.Cep,model.EnderecoCobranca.Telefone1,model.EnderecoCobranca.Telefone2,model.EnderecoCobranca.Email,model.CobrancaIgualCadastro,false),

                        new Endereco(model.EnderecoEntrega.IdEndereco,TipoEndereco.Entrega,model.EnderecoEntrega.Logradouro,model.EnderecoEntrega.Numero,model.EnderecoEntrega.Complemento,
                        model.EnderecoEntrega.Bairro,model.EnderecoEntrega.Municipio,model.EnderecoEntrega.UF,model.EnderecoEntrega.Cep,
                        model.EnderecoEntrega.Telefone1,model.EnderecoEntrega.Telefone2,model.EnderecoCobranca.Email,model.EntregaIgualCadastro,model.EntregaIgualCobranca)
                    };

                    var d = new ClienteDAL();

                    if (model.IdTransacao is null)
                    {
                        c.Acao = Acao.Cadastrar;
                        model.Acao = c.Acao;
                        if (!d.VerificarCNPJ(c.Cnpj))
                        {
                            d.CadastrarCliente(c);

                            TempData["Sucesso"] = true;
                            TempData["Resultado"] = "Solicitação de Cadastro enviada com sucesso.\n" +
                                "Um E-mail de confirmação foi enviado para você, assim que o cliente estiver cadastrado você receberá uma confirmação via E-mail.\n " +
                                "Se o cliente não estiver coligado a nenhum Codun já existente, clique no botão Cadastrar Modalidade Comercial.\n " +
                                $"IdTransação: {c.IdCliente}";
                            model.IdTransacao = c.IdCliente;

                            try
                            {
                                var r = new RepresentanteDAL();
                                List<string> destinatarios = r.ListaDestinatarios(c.Usuario.IdUsuario);

                                Email.EnviarEmailCadastroCliente(c, destinatarios);
                            }
                            catch (Exception e)
                            {
                                TempData["Sucesso"] = true;
                                TempData["Resultado"] = $"{e.Message} Se o cliente não estiver coligado a nenhum Codun já existente, clique no botão Cadastrar Modalidade Comercial.\n " +
                                $"IdTransação: {c.IdCliente}";
                            }
                        }
                        else
                        {
                            TempData["Sucesso"] = false;
                            TempData["Resultado"] = "Já existe um cliente cadastrado com o CNPJ informado";
                        }
                    }
                    
                    else 
                    {
                        c.Acao = Acao.Atualizar;
                        model.Acao = c.Acao;
                        c.IdCliente = (int)model.IdTransacao;
                        d.AtualizarCliente(c);

                        try
                        {
                            var r = new RepresentanteDAL();
                            List<string> destinatarios = r.ListaDestinatarios(c.Usuario.IdUsuario);

                            Email.EnviarEmailEdicaoCliente(c, destinatarios);

                            TempData["Sucesso"] = true;
                            TempData["Resultado"] = "Solicitação de atualização de dados cadastrais enviada com sucesso.\n" +
                                "Um E-mail de confirmação foi enviado para você, assim que o cliente estiver cadastrado você receberá uma confirmação via E-mail.";
                        }
                        catch (Exception e)
                        {
                            TempData["Sucesso"] = true;
                            TempData["Resultado"] = e.Message;
                        }                        
                    }
                }
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Resultado"] = "Operação não concluída. Erro: " + e.Message;
            }
            return View(model);
        }

        public JsonResult ConsultarCNPJ(string cnpj)
        {
            var model = new CadastroViewModel();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.receitaws.com.br/v1/cnpj/{cnpj}");
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

                    return Json(model);
                }
            }
            catch (Exception e)
            {
                model.Status = e.Message;
                return Json(model);
            }
        }

        public ActionResult Consulta()
        {
            return View(new ConsultaViewModel());
        }

        [HttpPost]
        public JsonResult Consulta(ConsultaViewModel model)
        {
            try
            {
                var lista = new List<ConsultaViewModel>();

                var d = new ClienteDAL();
                foreach (var c in d.ObterClientes(model.IdCliente, model.CodCliente, model.Codun, model.RazaoSocial,
                    model.NomeFantasia, model.Cnpj, model.IdAgente, model.IdPromotor, model.DataInicio, model.DataFim))
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
                    model.NomeAgente = c.Agente.Nome;
                    model.NomePromotor = c.Promotor.Nome;

                    foreach (var item in d.ObterEndereco(c.IdCliente))
                    {
                        switch (item.Tipo)
                        {
                            case (TipoEndereco.Cadastro):
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
                                model.EnderecoCadastro.DataCadastro = item.DataCadastro.ToString();
                                break;

                            case (TipoEndereco.Cobranca):
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
                                model.EnderecoCobranca.DataCadastro = item.DataCadastro.ToString();
                                break;

                            case (TipoEndereco.Entrega):
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
                                model.EnderecoEntrega.DataCadastro = item.DataCadastro.ToString();
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

        public ActionResult Visualizar(int id)
        {
            try
            {
                var model = new ConsultaViewModel();

                var d = new ClienteDAL(); 
                foreach (var item in d.ObterClientes(id))
                {

                }



            }
            catch (Exception e)
            {
                throw e;
            }
            return View();
        }
    }
}