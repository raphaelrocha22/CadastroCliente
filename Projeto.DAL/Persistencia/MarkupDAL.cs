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
        public int NumeroContrato(int codun)
        {
            AbrirConexao();

            string query = "select ISNULL(MAX(numeroContrato),0) numeroContrato from Markup where codun = @codun";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@codun", codun);
            int numeroContrato = Convert.ToInt32(cmd.ExecuteScalar()) + 1;

            FecharConexao();
            return numeroContrato;
        }

        public void Cadastrar(Markup c)
        {
            try
            {
                AbrirConexao();
                tr = con.BeginTransaction();

                string query = "insert into Markup (campanha,codun,numeroContrato,nomeResponsavel,cpfResponsavel," +
                    "modalidade,dataNegociacao,dataInicio,dataFim,mediaHistorica,periodoMeses,metaPeriodo,desconto,markup," +
                    "crescimento,mesesPagamentoRBR,mesesPagamentoNetline,netlineHabilitado,guelta,status,observacao,idUsuario,dataCadastro) " +
                    "values (@campanha,@codun,@numeroContrato,@nomeResponsavel,@cpfResponsavel," +
                    "@modalidade,@dataNegociacao,@dataInicio,@dataFim,@mediaHistorica,@periodoMeses,@metaPeriodo,@desconto,@markup," +
                    "@crescimento,@mesesPagamentoRBR,@mesesPagamentoNetline,@netlineHabilitado,@guelta,@status,@observacao,@idUsuario,@dataCadastro)";

                cmd = new SqlCommand(query, con, tr);
                cmd.Parameters.AddWithValue("@campanha", c.Campanha.ToString());
                cmd.Parameters.AddWithValue("@codun", c.Codun);
                cmd.Parameters.AddWithValue("@numeroContrato", c.NumeroContrato);
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
                cmd.Parameters.AddWithValue("@idUsuario", c.usuario.IdUsuario);
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
