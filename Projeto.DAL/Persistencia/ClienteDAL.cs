using Projeto.DAL.Repositorio;
using Projeto.Entidades;
using System;
using System.Data.SqlClient;


namespace Projeto.DAL.Persistencia
{
    public class ClienteDAL:Conexao
    {
        public bool VerificarCNPJ(string cnpj)
        {
            AbrirConexao();

            string query = "select idCliente from Cliente where cnpj = @cnpj";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@cnpj", cnpj);
            int qtd = Convert.ToInt32(cmd.ExecuteScalar());

            FecharConexao();

            return qtd > 0;            
        }


        public void CadastrarCliente(Cliente c)
        {
            try
            {
                AbrirConexao();
                tr = con.BeginTransaction();

                string query = "insert into Cliente (codun,razaoSocial,nomeFantasia,cnpj,inscricaoEstadual,inscricaoMunicipal,classe,idRepresentante) " +
                    "values (@codun,@razaoSocial,@nomeFantasia,@cnpj,@inscricaoEstadual,@inscricaoMunicipal,@classe,@idRepresentante) SELECT SCOPE_IDENTITY()";
                cmd = new SqlCommand(query, con, tr);
                cmd.Parameters.AddWithNullValue("@codun", c.codun);
                cmd.Parameters.AddWithNullValue("@razaoSocial", c.razaoSocial);
                cmd.Parameters.AddWithNullValue("@nomeFantasia", c.nomeFantasia);
                cmd.Parameters.AddWithNullValue("@cnpj", c.cnpj);
                cmd.Parameters.AddWithNullValue("@inscricaoEstadual",c.inscricaoEstadual);
                cmd.Parameters.AddWithNullValue("inscricaoMunicipal",c.inscricaoMunicipal);
                cmd.Parameters.AddWithNullValue("@classe", c.classe);
                cmd.Parameters.AddWithNullValue("idRepresentante", c.representante.idRepresentante);
                c.idCliente = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var e in c.endereco)
                {
                    query = "insert into Endereco (logradouro,numero,complemento,bairro,municipio,uf,cep,telefone1,telefone2,email,tipo,idCliente) " +
                        "values (@logradouro,@numero,@complemento,@bairro,@municipio,@uf,@cep,@telefone1,@telefone2,@email,@tipo,@idCliente)";
                    cmd = new SqlCommand(query, con, tr);
                    cmd.Parameters.AddWithNullValue("@logradouro", e.logradouro);
                    cmd.Parameters.AddWithNullValue("@numero", e.numero);
                    cmd.Parameters.AddWithNullValue("@complemento",e.complemento);
                    cmd.Parameters.AddWithNullValue("@bairro", e.bairro);
                    cmd.Parameters.AddWithNullValue("@municipio", e.municipio);
                    cmd.Parameters.AddWithNullValue("@uf", e.UF);
                    cmd.Parameters.AddWithNullValue("@cep", e.cep);
                    cmd.Parameters.AddWithNullValue("@telefone1", e.telefone1);
                    cmd.Parameters.AddWithNullValue("@telefone2", e.telefone2);
                    cmd.Parameters.AddWithNullValue("@email", e.email);
                    cmd.Parameters.AddWithNullValue("@tipo", e.tipo);
                    cmd.Parameters.AddWithNullValue("@idCliente", c.idCliente);
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
