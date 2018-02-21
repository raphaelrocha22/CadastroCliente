using Projeto.Entidades;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Projeto.Util
{
    public class Email
    {
        private static void EnviarEmail(MailMessage message, List<string> destinatarios)
        {
            try
            {
                //foreach (var item in destinatarios)
                //{
                //    message.To.Add(item);
                //}
                message.To.Add("raphael.2205@gmail.com");
                message.From = new MailAddress("raphaelportfolio22@gmail.com", "Raphael portfolio");

                //Configuracoes
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true; // GMail requer SSL
                smtp.Port = 587;       // porta para SSL
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // modo de envio
                smtp.UseDefaultCredentials = false; // vamos utilizar credencias especificas

                // seu usuário e senha para autenticação
                smtp.Credentials = new NetworkCredential("raphaelportfolio22@gmail.com", "raphael22");
                smtp.Send(message);
            }
            catch
            {
                throw new Exception("A solicitação foi realizada com sucesso, mas não conseguimos enviar para você o email de confirmação. " +
                    "Por favor, entre em contato com o Back Office do RJ para confirmar o recebimento.");
            }
        }

        public static void EnviarEmailClubR(ClubR c, List<string> destinatarios)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.Subject = $"Solicitação de cadastro cliente ClubR - {c.Codun}";

                var contratoAnexo = c.FileContrato is null ? "Não" : $"Sim - Nome do Arquivo: {c.FileContrato}";
                var compraNetline = c.NetlineHabilitado ? $"Sim, em {c.MesesPagamentoNetline} vezes" : "Não";

                message.Body =
                    "Foi solicitado um novo cadastro de Cliente na Campanha ClubR \n\n" +
                    $"Codun: {c.Codun} \n" +
                    $"Contrato: {c.Versao} \n" +
                    $"Nome Responsável: {c.NomeResponsavel} \n" +
                    $"CPF Responsavel: {c.CpfResponsavel} \n" +
                    $"Data Negociação: {c.DataNegociacao.ToShortDateString()} \n" +
                    $"Modalidade: {c.Modalidade} \n" +
                    $"Período do Contrato: {c.PeriodoMeses} meses \n" +
                    $"Data Início: {c.DataInicio.ToShortDateString()} \n" +
                    $"Data Fim: {c.DataFim.ToShortDateString()} \n" +
                    $"Média Histórica Mensal: {string.Format("{0:C}", c.MediaHistorica)} \n" +
                    $"Meta para o Período: {string.Format("{0:C}", c.MetaPeriodo)} \n" +
                    $"Crescimento Proposto: {c.Crescimento} % \n" +
                    $"Prazo Pagamento RBR: {c.MesesPagamentoRBR} vezes \n" +
                    $"Habiitada Compra Netline ?: {compraNetline} \n" +
                    $"MarkUP: {c.Markup} \n" +
                    $"Desconto: {Math.Round(c.Desconto * 100, 1)}% \n" +
                    $"Guelta: {c.Guelta} \n" +
                    $"Rebate: {c.RebatePercent * 100}% \n" +
                    $"Rebate Valor: {string.Format("{0:C}", c.RebateValor)}\n" +
                    $"Contrato assinado foi anexado: {contratoAnexo} \n" +
                    $"Observação: {c.Observacao} \n\n" +
                    $"Usuário: {c.Usuario.Nome} \n" +
                    $"Data: {DateTime.Now} \n\n" +

                    $"Este é um email automático do sistema, por favor não responda, " +
                    $"qualquer problema, encaminhe este email para raphael.rocha@rodenstock.com.br e explique o ocorrido.";

                EnviarEmail(message, destinatarios);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void EnviarEmailUpgrade(Upgrade c, List<string> destinatarios)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.Subject = $"Solicitação de cadastro cliente Upgrade - {c.Codun}";

                var compraNetline = c.NetlineHabilitado ? $"Sim, em {c.MesesPagamentoNetline} vezes" : "Não";

                message.Body =
                    "Foi solicitado um novo cadastro de Cliente na Campanha Upgrade \n\n" +
                    $"Codun: {c.Codun} \n" +
                    $"Contrato: {c.Versao} \n" +
                    $"Nome Responsável: {c.NomeResponsavel} \n" +
                    $"CPF Responsavel: {c.CpfResponsavel} \n" +
                    $"Data Negociação: {c.DataNegociacao.ToShortDateString()} \n" +
                    $"Modalidade: {c.Modalidade} \n" +
                    $"Período do Contrato: {c.PeriodoMeses} meses \n" +
                    $"Data Início: {c.DataInicio.ToShortDateString()} \n" +
                    $"Data Fim: {c.DataFim.ToShortDateString()} \n" +
                    $"Média Histórica Mensal: {string.Format("{0:C}", c.MediaHistorica)} \n" +
                    $"Meta para o Período: {string.Format("{0:C}", c.MetaPeriodo)} \n" +
                    $"Crescimento Proposto: {c.Crescimento} % \n" +
                    $"Prazo Pagamento RBR: {c.MesesPagamentoRBR} vezes \n" +
                    $"Habiitada Compra Netline ?: {compraNetline} \n" +
                    $"MarkUP: {c.Markup} \n" +
                    $"Desconto: {Math.Round(c.Desconto * 100, 1)}% \n" +
                    $"Guelta: {c.Guelta} \n" +
                    $"Observação: {c.Observacao} \n\n" +
                    $"Usuário: {c.Usuario.Nome} \n" +
                    $"Data: {DateTime.Now} \n\n" +

                    $"Este é um email automático do sistema, por favor não responda, " +
                    $"qualquer problema, encaminhe este email para raphael.rocha@rodenstock.com.br e explique o ocorrido.";

                EnviarEmail(message, destinatarios);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void EnviarEmailMarkup(Markup c, List<string> destinatarios)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.Subject = $"Solicitação de cadastro cliente Markup - {c.Codun}";

                var compraNetline = c.NetlineHabilitado ? $"Sim, em {c.MesesPagamentoNetline} vezes" : "Não";

                message.Body =
                    "Foi solicitado um novo cadastro de Cliente na Campanha MArkup \n\n" +
                    $"Codun: {c.Codun} \n" +
                    $"Contrato: {c.Versao} \n" +
                    $"Nome Responsável: {c.NomeResponsavel} \n" +
                    $"CPF Responsavel: {c.CpfResponsavel} \n" +
                    $"Data Negociação: {c.DataNegociacao.ToShortDateString()} \n" +
                    $"Modalidade: {c.Modalidade} \n" +
                    $"Período do Contrato: {c.PeriodoMeses} meses \n" +
                    $"Data Início: {c.DataInicio.ToShortDateString()} \n" +
                    $"Data Fim: {c.DataFim.ToShortDateString()} \n" +
                    $"Média Histórica Mensal: {string.Format("{0:C}", c.MediaHistorica)} \n" +
                    $"Meta para o Período: {string.Format("{0:C}", c.MetaPeriodo)} \n" +
                    $"Crescimento Proposto: {c.Crescimento} % \n" +
                    $"Prazo Pagamento RBR: {c.MesesPagamentoRBR} vezes \n" +
                    $"Habiitada Compra Netline ?: {compraNetline} \n" +
                    $"MarkUP: {c.Markup} \n" +
                    $"Desconto: {Math.Round(c.Desconto * 100, 1)}% \n" +
                    $"Guelta: {c.Guelta} \n" +
                    $"Observação: {c.Observacao} \n\n" +
                    $"Usuário: {c.Usuario.Nome} \n" +
                    $"Data: {DateTime.Now} \n\n" +

                    $"Este é um email automático do sistema, por favor não responda, " +
                    $"qualquer problema, encaminhe este email para raphael.rocha@rodenstock.com.br e explique o ocorrido.";

                EnviarEmail(message, destinatarios);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void EnviarEmailSemCampanha(SemCampanha c, List<string> destinatarios)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.Subject = $"Solicitação de cadastro cliente Sem Campanha - #CodTransação: {c.Cliente.IdCliente}";

                var compraNetline = c.NetlineHabilitado ? $"Sim, em {c.MesesPagamentoNetline} vezes" : "Não";

                message.Body =
                    "Foi solicitado um novo cadastro de Cliente Sem Campanha \n\n" +
                    $"Codun: {c.Cliente.Codun} \n" +
                    $"Contrato: {c.Versao} \n" +
                    $"Data Negociação: {c.DataNegociacao.ToShortDateString()} \n" +
                    $"Data Início: {c.DataInicio.ToShortDateString()} \n" +
                    $"Prazo Pagamento RBR: {c.MesesPagamentoRBR} vezes \n" +
                    $"Habiitada Compra Netline ?: {compraNetline} \n" +
                    $"MarkUP: {c.Markup} \n" +
                    $"Desconto: {Math.Round(c.Desconto * 100, 1)}% \n" +
                    $"Guelta: {c.Guelta} \n" +
                    $"Observação: {c.Observacao} \n\n" +
                    $"Usuário: {c.Usuario.Nome} \n" +
                    $"Data: {DateTime.Now} \n\n" +

                    $"Este é um email automático do sistema, por favor não responda, " +
                    $"qualquer problema, encaminhe este email para raphael.rocha@rodenstock.com.br e explique o ocorrido.";

                EnviarEmail(message, destinatarios);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void EnviarEmailCadastroCliente(Cliente c, List<string> destinatarios)
        {
            try
            {
                string endereco = string.Empty;
                foreach (var item in c.Enderecos)
                {
                    endereco += 
                    $"Tipo Endereço: {item.Tipo.ToString()} \n" +
                    $"Logradouro: {item.Logradouro} \n" +
                    $"Numero: {item.Numero} \n" +
                    $"Complemento: {item.Complemento} \n" +
                    $"Bairro: {item.Bairro} \n" +
                    $"Municipio: {item.Municipio} \n" +
                    $"UF: {item.UF} \n" +
                    $"Cep: {item.Cep} \n" +
                    $"Telefone1: {item.Telefone1} \n" +
                    $"Telefone2: {item.Telefone2} \n" +
                    $"Email: {item.Email} \n\n";
                }

                MailMessage message = new MailMessage();
                message.Subject = $"Solicitação de cadastro de novo Cliente - #CodTransacao: {c.IdCliente}";

                message.Body =
                    "Foi solicitado um novo cadastro de Cliente \n\n" +

                    $"CodTransação: {c.IdCliente} \n" +
                    $"Codun: {c.Codun} \n" +
                    $"Razão Social: {c.RazaoSocial} \n" +
                    $"Nome Fantasia: {c.NomeFantasia} \n" +
                    $"CNPJ: {c.Cnpj} \n" +
                    $"Inscrição Estadual: {c.InscricaoEstadual} \n" +
                    $"Inscrição Municipal: {c.InscricaoMunicipal} \n" +
                    $"Classe: {c.Classe.ToString()} \n" +
                    $"Observação: {c.Observacao} \n" +
                    $"Representante: {c.Agente.Nome} \n\n" +
                    
                    $"{endereco}" +
                                      
                    $"Usuário: {c.Usuario.Nome} \n" +
                    $"Data: {DateTime.Now} \n\n" +

                    $"Este é um email automático do sistema, por favor não responda, " +
                    $"qualquer problema, encaminhe este email para raphael.rocha@rodenstock.com.br e explique o ocorrido.";

                EnviarEmail(message, destinatarios);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static void EnviarEmailEdicaoCliente(Cliente c, List<string> destinatarios)
        {
            try
            {
                string endereco = string.Empty;
                foreach (var item in c.Enderecos)
                {
                    endereco +=
                    $"Tipo Endereço: {item.Tipo.ToString()} \n" +
                    $"Logradouro: {item.Logradouro} \n" +
                    $"Numero: {item.Numero} \n" +
                    $"Complemento: {item.Complemento} \n" +
                    $"Bairro: {item.Bairro} \n" +
                    $"Municipio: {item.Municipio} \n" +
                    $"UF: {item.UF} \n" +
                    $"Cep: {item.Cep} \n" +
                    $"Telefone1: {item.Telefone1} \n" +
                    $"Telefone2: {item.Telefone2} \n" +
                    $"Email: {item.Email} \n\n";
                }

                MailMessage message = new MailMessage();
                message.Subject = $"Solicitação de Edição de Cliente - CodCliente: {c.CodCliente} - #CodTransacao: {c.IdCliente}";

                message.Body =
                    "Foi solicitada a edição de dados cadastrais de um Cliente \n\n" +

                    $"CodTransação: {c.IdCliente} \n" +
                    $"CodCliente: {c.CodCliente} \n" +
                    $"Codun: {c.Codun} \n" +
                    $"Razão Social: {c.RazaoSocial} \n" +
                    $"Nome Fantasia: {c.NomeFantasia} \n" +
                    $"CNPJ: {c.Cnpj} \n" +
                    $"Inscrição Estadual: {c.InscricaoEstadual} \n" +
                    $"Inscrição Municipal: {c.InscricaoMunicipal} \n" +
                    $"Classe: {c.Classe.ToString()} \n" +
                    $"Observação: {c.Observacao} \n" +
                    $"Representante: {c.Agente.Nome} \n\n" +

                    $"{endereco}" +

                    $"Usuário: {c.Usuario.Nome} \n" +
                    $"Data: {DateTime.Now} \n\n" +

                    $"Este é um email automático do sistema, por favor não responda, " +
                    $"qualquer problema, encaminhe este email para raphael.rocha@rodenstock.com.br e explique o ocorrido.";

                EnviarEmail(message, destinatarios);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
