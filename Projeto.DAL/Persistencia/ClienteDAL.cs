using Projeto.DAL.Repositorio;
using Projeto.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Projeto.DAL.Persistencia
{
    public class ClienteDAL : Conexao
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
                cmd.Parameters.AddWithValue("@razaoSocial", c.razaoSocial);
                cmd.Parameters.AddWithNullValue("@nomeFantasia", c.nomeFantasia);
                cmd.Parameters.AddWithValue("@cnpj", c.cnpj);
                cmd.Parameters.AddWithNullValue("@inscricaoEstadual", c.inscricaoEstadual);
                cmd.Parameters.AddWithNullValue("inscricaoMunicipal", c.inscricaoMunicipal);
                cmd.Parameters.AddWithValue("@classe", c.classe);
                cmd.Parameters.AddWithValue("idRepresentante", c.representante.idRepresentante);
                c.idCliente = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var e in c.endereco)
                {
                    query = "insert into Endereco (logradouro,numero,complemento,bairro,municipio,uf,cep,telefone1,telefone2,email,tipo,idCliente) " +
                        "values (@logradouro,@numero,@complemento,@bairro,@municipio,@uf,@cep,@telefone1,@telefone2,@email,@tipo,@idCliente)";
                    cmd = new SqlCommand(query, con, tr);
                    cmd.Parameters.AddWithValue("@logradouro", e.logradouro);
                    cmd.Parameters.AddWithValue("@numero", e.numero);
                    cmd.Parameters.AddWithNullValue("@complemento", e.complemento);
                    cmd.Parameters.AddWithValue("@bairro", e.bairro);
                    cmd.Parameters.AddWithValue("@municipio", e.municipio);
                    cmd.Parameters.AddWithValue("@uf", e.UF);
                    cmd.Parameters.AddWithValue("@cep", e.cep);
                    cmd.Parameters.AddWithNullValue("@telefone1", e.telefone1);
                    cmd.Parameters.AddWithNullValue("@telefone2", e.telefone2);
                    cmd.Parameters.AddWithNullValue("@email", e.email);
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

        public List<Cliente> ObterClientes(Cliente cliente)
        {
            try
            {
                AbrirConexao();

                string query = "select idCliente, codCliente, codun, razaoSocial, nomeFantasia, cnpj, inscricaoEstadual, " +
                    "inscricaoMunicipal, classe, c.idRepresentante, r.nome " +
                    "from Cliente c inner join Representante r on c.idRepresentante = r.idRepresentante " +
                    "where idCliente>0 ";
                cmd = new SqlCommand(query, con);

                if (cliente.codCliente != 0)
                    query += "and codCliente = @codCliente";

                if (cliente.codun != 0)
                    query += "and codun = @codun";

                if (cliente.razaoSocial != null)
                    query += "and razaoSocial like '%" + "@razaoSocial" + "%'";

                if (cliente.nomeFantasia != null)
                    query += "and nomeFantasia like '%" + "@nomeFantasia" + "%'";

                if (cliente.cnpj != null)
                    query += "and cnpj = @cnpj";

                if (cliente.representante.idRepresentante != 0)
                    query += "and c.idRepresentante = @idRepresentante";

                cmd.Parameters.AddWithNullValue("@codCliente", cliente.codCliente);
                cmd.Parameters.AddWithNullValue("@codun", cliente.codun);
                cmd.Parameters.AddWithNullValue("@razaoSocial", cliente.razaoSocial);
                cmd.Parameters.AddWithNullValue("@nomeFantasia", cliente.nomeFantasia);
                cmd.Parameters.AddWithNullValue("@cnpj", cliente.cnpj);
                cmd.Parameters.AddWithNullValue("@idRepresentante", cliente.representante.idRepresentante);
                dr = cmd.ExecuteReader();

                var lista = new List<Cliente>();

                while (dr.Read())
                {
                    var c = new Cliente();
                    c.representante = new Representante();
                    c.endereco = new List<Endereco>();

                    c.idCliente = (int)dr["idCliente"];
                    c.codCliente = (int)dr["codCliente"];
                    c.razaoSocial = (string)dr["razaoSocial"];
                    c.nomeFantasia = (string)dr["nomeFantasia"];
                    c.cnpj = (string)dr["cnpj"];
                    c.inscricaoEstadual = (string)dr["inscricaoEstadual"];
                    c.inscricaoMunicipal = (string)dr["inscricaoMunicipal"];
                    c.classe = (string)dr["classe"];
                    c.representante.idRepresentante = (int)dr["idRepresentante"];
                    c.representante.nome = (string)dr["nome"];

                    dr.Dispose();
                    query = "select idEndereco, logradouro, numero, complemento, bairro, municipio, " +
                        "uf, cep, telefone1, telefone2, email, tipo FROM Endereco where idCliente = @idCliente";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@idCliente", c.idCliente);
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        var e = new Endereco();
                        e.idEndereco = (int)dr["idEndereco"];
                        e.logradouro = (string)dr["logradouro"];
                        e.numero = (string)dr["numero"];
                        e.complemento = (string)dr["complemento"];
                    }

                }

            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                FecharConexao();
            }
        }


        public void AtualizarCliente(Cliente c)
        {
            try
            {
                AbrirConexao();
                tr = con.BeginTransaction();

                string query = "update Cliente set codCliente = @codCliente, codun = @codun, razaoSocial = @razaoSocial, " +
                    "nomeFantasia = @nomeFantasia, cnpj = @cnpj, inscricaoEstadual = @inscricaoEstadual, " +
                    "inscricaoMunicipal = @inscricaoMunicipal, classe = @classe, idRepresentante = @idRepresentante " +
                    "where idCliente = @idCliente";
                cmd = new SqlCommand(query, con, tr);
                cmd.Parameters.AddWithNullValue("@codCliente", c.codCliente);
                cmd.Parameters.AddWithNullValue("@codun", c.codun);
                cmd.Parameters.AddWithValue("@razaoSocial", c.razaoSocial);
                cmd.Parameters.AddWithNullValue("@nomeFantasia", c.nomeFantasia);
                cmd.Parameters.AddWithValue("@cnpj", c.cnpj);
                cmd.Parameters.AddWithNullValue("@inscricaoEstadual", c.inscricaoEstadual);
                cmd.Parameters.AddWithNullValue("@inscricaoMunicipal", c.inscricaoMunicipal);
                cmd.Parameters.AddWithValue("@classe", c.classe);
                cmd.Parameters.AddWithValue("@idRepresentante", c.representante.idRepresentante);
                cmd.Parameters.AddWithValue("@idCliente", c.idCliente);
                cmd.ExecuteNonQuery();

                foreach (var e in c.endereco)
                {
                    query = "update Endereco set logradouro = @logradouro, numero = @numero, complemento = @complemento, " +
                        "bairro = @bairro, municipio = @municipio, uf = @uf, cep = @cep, telefone1 = @telefone1, " +
                        "telefone2 = @telefone2, email = @email, tipo = @tipo, idCliente = @idCliente where idEndereco = @idEndereco";
                    cmd = new SqlCommand(query, con, tr);
                    cmd.Parameters.AddWithValue("@logradouro", e.logradouro);
                    cmd.Parameters.AddWithValue("@numero", e.numero);
                    cmd.Parameters.AddWithNullValue("@complemento", e.complemento);
                    cmd.Parameters.AddWithValue("@bairro", e.bairro);
                    cmd.Parameters.AddWithValue("@municipio", e.municipio);
                    cmd.Parameters.AddWithValue("@uf", e.UF);
                    cmd.Parameters.AddWithValue("@cep", e.cep);
                    cmd.Parameters.AddWithNullValue("@telefone1", e.telefone1);
                    cmd.Parameters.AddWithNullValue("@telefone2", e.telefone2);
                    cmd.Parameters.AddWithNullValue("@email", e.email);
                    cmd.Parameters.AddWithValue("@tipo", e.tipo);
                    cmd.Parameters.AddWithValue("@idCliente", c.idCliente);
                    cmd.Parameters.AddWithValue("@idEndereco", e.idEndereco);
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
