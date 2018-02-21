using Projeto.DAL.Repositorio;
using Projeto.Entidades;
using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Persistencia
{
    public class CentralSolicitacaoDAL : Conexao
    {
        public List<CentralSolicitacao>Consultar(StatusSolicitacao status)
        {
            var lista = new List<CentralSolicitacao>();

            var clientes = ObterClientes(lista, status);
            var campanhas = ObterCampanha(lista, status);

            return lista;
        }
        private List<CentralSolicitacao> ObterClientes(List<CentralSolicitacao> lista, StatusSolicitacao status)
        {
            try
            {
                AbrirConexao();
                                
                string query = "select idCliente, 'Cliente' tabela, ISNULL(codCliente,0)codCliente, ISNULL(codun,0)codun, u.nome 'Usuario',acao from Cliente c " +
                    "inner join Usuario u on c.idUsuario = u.idUsuario " +
                    "where c.status = @status";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@status", status.ToString());
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var c = new CentralSolicitacao();
                    c.Id = (int)dr["idCliente"];
                    c.Tabela = dr["tabela"].ToString();
                    c.CodCliente = (int)dr["codCliente"];
                    c.Codun = (int)dr["codun"];
                    c.Acao = (Acao)Enum.Parse(typeof(Acao), dr["acao"].ToString());
                    c.NomeUsuario = dr["Usuario"].ToString();

                    lista.Add(c);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }
        private List<CentralSolicitacao> ObterCampanha(List<CentralSolicitacao> lista, StatusSolicitacao status)
        {
            try
            {
                AbrirConexao();

                string query = "select id, 'Campanha' tabela,campanha, ISNULL(codCliente,0)codCliente, ISNULL(codun,0)codun, u.nome 'Usuario',acao from Campanha c " +
                    "inner join Usuario u on c.idUsuario = u.idUsuario " +
                    "where c.status = @status";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@status", status.ToString());
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var c = new CentralSolicitacao();
                    c.Id = (int)dr["id"];
                    c.Tabela = dr["tabela"].ToString();
                    c.Campanha = (Campanha)Enum.Parse(typeof(Campanha), dr["campanha"].ToString());
                    c.CodCliente = (int)dr["codCliente"];
                    c.Codun = (int)dr["codun"];
                    c.Acao = (Acao)Enum.Parse(typeof(Acao), dr["acao"].ToString());
                    c.NomeUsuario = dr["Usuario"].ToString();

                    lista.Add(c);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }

    }
}
