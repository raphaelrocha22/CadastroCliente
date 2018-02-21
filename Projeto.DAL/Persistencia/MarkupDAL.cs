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
    public class MarkupDAL:Conexao
    {
        public int Versao(int codun)
        {
            AbrirConexao();

            string query = "select ISNULL(MAX(versao),0) versao from Campanha where codun = @codun";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@codun", codun);
            int versao = Convert.ToInt32(cmd.ExecuteScalar()) + 1;

            FecharConexao();
            return versao;
        }

        public void Cadastrar(Markup c)
        {
            try
            {
                AbrirConexao();
                tr = con.BeginTransaction();

                string query = "insert into Campanha (campanha,codun,versao,nomeResponsavel,cpfResponsavel," +
                    "modalidade,dataNegociacao,dataInicio,dataFim,mediaHistorica,periodoMeses,metaPeriodo,desconto,markup," +
                    "crescimento,mesesPagamentoRBR,mesesPagamentoNetline,netlineHabilitado,guelta,status,observacao,idUsuario,dataCadastro,acao) " +
                    "values (@campanha,@codun,@versao,@nomeResponsavel,@cpfResponsavel," +
                    "@modalidade,@dataNegociacao,@dataInicio,@dataFim,@mediaHistorica,@periodoMeses,@metaPeriodo,@desconto,@markup," +
                    "@crescimento,@mesesPagamentoRBR,@mesesPagamentoNetline,@netlineHabilitado,@guelta,@status,@observacao,@idUsuario,@dataCadastro,@acao)";

                cmd = new SqlCommand(query, con, tr);
                cmd.Parameters.AddWithValue("@campanha", c.Campanha.ToString());
                cmd.Parameters.AddWithValue("@codun", c.Codun);
                cmd.Parameters.AddWithValue("@versao", c.Versao);
                cmd.Parameters.AddWithNullValue("@nomeResponsavel", c.NomeResponsavel);
                cmd.Parameters.AddWithNullValue("@cpfResponsavel", c.CpfResponsavel);
                cmd.Parameters.AddWithValue("@modalidade", c.Modalidade.ToString());
                cmd.Parameters.AddWithNullValue("@dataNegociacao", c.DataNegociacao);
                cmd.Parameters.AddWithValue("@dataInicio", c.DataInicio);
                cmd.Parameters.AddWithValue("@dataFim", c.DataFim);
                cmd.Parameters.AddWithValue("@mediaHistorica", c.MediaHistorica);
                cmd.Parameters.AddWithValue("@periodoMeses", c.PeriodoMeses);
                cmd.Parameters.AddWithValue("@metaPeriodo", c.MetaPeriodo);
                cmd.Parameters.AddWithValue("@desconto", c.Desconto);
                cmd.Parameters.AddWithValue("@crescimento", c.Crescimento);
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
