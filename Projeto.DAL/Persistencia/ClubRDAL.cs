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
    public class ClubRDAL : Conexao
    {
        public int NumeroContrato(int codun)
        {
            AbrirConexao();

            string query = "select ISNULL(MAX(numeroContrato),0) numeroContrato from clubR where codun = @codun";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@codun", codun);
            int numeroContrato = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
            
            FecharConexao();
            return numeroContrato;
        }
        
        public void Cadastrar(ClubR c)
        {
            try
            {
                AbrirConexao();
                tr = con.BeginTransaction();

                string query = "insert into clubR (programa,codun,numeroContrato,nomeResponsavel,cpfResponsavel," +
                    "modalidade,dataNegociacao,dataInicio,dataFim,mediaHistorica,periodoMeses,metaPeriodo,desconto," +
                    "crescimento,prazoPagamento,rebateValor,rebatePercent,ativo,contrato,observacao) values (@programa,@codun,@numeroContrato,@nomeResponsavel,@cpfResponsavel," +
                    "@modalidade,@dataNegociacao,@dataInicio,@dataFim,@mediaHistorica,@periodoMeses,@metaPeriodo,@desconto," +
                    "@crescimento,@prazoPagamento,@rebateValor,@rebatePercent,@ativo,@contrato,@observacao)";

                cmd = new SqlCommand(query, con, tr);
                cmd.Parameters.AddWithValue("@programa", c.Programa);
                cmd.Parameters.AddWithValue("@codun", c.Codun);
                cmd.Parameters.AddWithValue("@numeroContrato", c.NumeroContrato);
                cmd.Parameters.AddWithValue("@nomeResponsavel", c.NomeResponsavel);
                cmd.Parameters.AddWithValue("@cpfResponsavel", c.CpfResponsavel);
                cmd.Parameters.AddWithValue("@modalidade", c.Modalidade);
                cmd.Parameters.AddWithValue("@dataNegociacao", c.DataNegociacao);
                cmd.Parameters.AddWithValue("@dataInicio", c.DataInicio);
                cmd.Parameters.AddWithValue("@dataFim", c.DataFim);
                cmd.Parameters.AddWithValue("@mediaHistorica", c.MediaHistorica);
                cmd.Parameters.AddWithValue("@periodoMeses", c.PeriodoMeses);
                cmd.Parameters.AddWithValue("@metaPeriodo", c.MetaPeriodo);
                cmd.Parameters.AddWithValue("@desconto", c.Desconto);
                cmd.Parameters.AddWithValue("@crescimento", c.Crescimento);
                cmd.Parameters.AddWithValue("@prazoPagamento", c.PrazoPagamento);
                cmd.Parameters.AddWithValue("@rebateValor", c.RebateValor);
                cmd.Parameters.AddWithValue("@rebatePercent", c.RebatePercent);
                cmd.Parameters.AddWithValue("@ativo", c.Ativo);
                cmd.Parameters.AddWithValue("@contrato", c.Contrato);
                cmd.Parameters.AddWithValue("@observacao", c.Observacao);
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
