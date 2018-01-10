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
    public class ClienteDAL : Conexao
    {
        public void CadastrarCliente(Cliente c)
        {
            try
            {
                AbrirConexao();
                tr = con.BeginTransaction();

                string query = "insert into Cliente (codun,razaoSocial,nomeFantasia,cnpj,inscricaoEstadual,inscricaoMunicipal,classe,idRepresentante) " +
                    "values (@codun,@razaoSocial,@nomeFantasia,@cnpj,@inscricaoEstadual,@inscricaoMunicipal,@classe,@idRepresentante) SELECT SCOPE_IDENTITY()";
                cmd = new SqlCommand(query, con, tr);
                cmd.Parameters.AddWithValue("@codun", c.codun);
                cmd.Parameters.AddWithValue("@razaoSocial", c.razaoSocial);
                cmd.Parameters.AddWithValue("@nomeFantasia", c.nomeFantasia);
                cmd.Parameters.AddWithValue("@cnpj", c.cnpj);
                cmd.Parameters.AddWithValue("@inscricaoEstadual", c.inscricaoEstadual);
                cmd.Parameters.AddWithValue("inscricaoMunicipal", c.inscricaoMunicipal);
                cmd.Parameters.AddWithValue("@classe", c.classe);
                cmd.Parameters.AddWithValue("idRepresentante", c.representante);
                c.idCliente = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var e in c.enderecos)
                {
                    query = "insert into Endereco (logradouro,numero,complemento,bairro,municipio,uf,cep,telefone1,telefone2,email,tipo,idCliente) " +
                        "values (@logradouro,@numero,@complemento,@bairro,@municipio,@uf,@cep,@telefone1,@telefone2,@email,@tipo,@idCliente)";
                    cmd = new SqlCommand(query, con, tr);
                    cmd.Parameters.AddWithValue("@logradouro", e.logradouro);
                    cmd.Parameters.AddWithValue("@numero", e.numero);
                    cmd.Parameters.AddWithValue("@complemento", e.complemento);
                    cmd.Parameters.AddWithValue("@bairro", e.bairro);
                    cmd.Parameters.AddWithValue("@municipio", e.municipio);
                    cmd.Parameters.AddWithValue("@uf", e.UF);
                    cmd.Parameters.AddWithValue("@cep", e.cep);
                    cmd.Parameters.AddWithValue("@telefone1", e.telefone1);
                    cmd.Parameters.AddWithValue("@telefone2", e.telefone2);
                    cmd.Parameters.AddWithValue("@email", e.email);
                    cmd.Parameters.AddWithValue("@tipo", e.tipo);
                    cmd.Parameters.AddWithValue("@idCliente", c.idCliente);
                    cmd.ExecuteNonQuery();
                }
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
