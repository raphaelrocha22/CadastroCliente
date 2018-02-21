using Projeto.DAL.Repositorio;
using Projeto.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Persistencia
{
    public class SemCampanhaDAL:Conexao
    {
        public int Versao(int codun, int codCliente, int idTransacao)
        {
            AbrirConexao();

            string query = "select ISNULL(MAX(versao),0) versao from Campanha where ";

            if (codun != 0)
                query += "codun = @codun";

            if (codCliente != 0)
                query += "codCliente = @codCliente";

            if (idTransacao != 0)
                query += "idCliente = @idTransacao";

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@codun", codun);
            cmd.Parameters.AddWithValue("@codCliente", codCliente);
            cmd.Parameters.AddWithValue("@idTransacao", idTransacao);
            int versao = Convert.ToInt32(cmd.ExecuteScalar()) + 1;

            FecharConexao();
            return versao;
        }

        public void Cadastrar(SemCampanha c)
        {
            try
            {
                AbrirConexao();
                tr = con.BeginTransaction();

                string query = "insert into Campanha (campanha,idCliente,codCliente,codun,versao," +
                    "dataNegociacao,dataInicio,desconto,markup, mesesPagamentoRBR,mesesPagamentoNetline, " +
                    "netlineHabilitado,guelta,status,observacao,idUsuario,dataCadastro,acao) " +
                    "values (@campanha,@idCliente,@codCliente,@codun,@versao,@dataNegociacao,@dataInicio,@desconto,@markup," +
                    "@mesesPagamentoRBR,@mesesPagamentoNetline,@netlineHabilitado,@guelta,@status,@observacao,@idUsuario,@dataCadastro,@acao)";

                cmd = new SqlCommand(query, con, tr);
                cmd.Parameters.AddWithValue("@campanha", c.Campanha.ToString());
                cmd.Parameters.AddWithValue("@idCliente", c.Cliente.IdCliente);
                cmd.Parameters.AddWithValue("@codCliente", c.Cliente.CodCliente);
                cmd.Parameters.AddWithValue("@codun", c.Cliente.Codun);
                cmd.Parameters.AddWithValue("@versao", c.Versao);
                cmd.Parameters.AddWithNullValue("@dataNegociacao", c.DataNegociacao);
                cmd.Parameters.AddWithValue("@dataInicio", c.DataInicio);
                cmd.Parameters.AddWithValue("@desconto", c.Desconto);
                cmd.Parameters.AddWithValue("@markup", c.Markup);
                cmd.Parameters.AddWithValue("@mesesPagamentoRBR", c.MesesPagamentoRBR);
                cmd.Parameters.AddWithValue("@mesesPagamentoNetline", c.MesesPagamentoNetline);
                cmd.Parameters.AddWithValue("@netlineHabilitado", c.NetlineHabilitado);
                cmd.Parameters.AddWithValue("@guelta", c.Guelta.ToString());
                cmd.Parameters.AddWithValue("@status", c.Status.ToString());
                cmd.Parameters.AddWithNullValue("@observacao", c.Observacao);
                cmd.Parameters.AddWithValue("@acao", c.Acao.ToString());
                cmd.Parameters.AddWithValue("@idUsuario", c.Usuario.IdUsuario);
                cmd.Parameters.AddWithValue("@dataCadastro", DateTime.Now);
                cmd.ExecuteNonQuery();

                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }


    }
}
