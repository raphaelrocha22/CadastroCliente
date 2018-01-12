using Projeto.DAL.Repositorio;
using Projeto.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;

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

                string query = "insert into Cliente (codun,razaoSocial,nomeFantasia,cnpj,inscricaoEstadual,inscricaoMunicipal,classe,idRepresentante, ativo, dataCadastro) " +
                    "values (@codun,@razaoSocial,@nomeFantasia,@cnpj,@inscricaoEstadual,@inscricaoMunicipal,@classe,@idRepresentante,@ativo,@dataCadastro) SELECT SCOPE_IDENTITY()";
                cmd = new SqlCommand(query, con, tr);
                cmd.Parameters.AddWithNullValue("@codun", c.codun);
                cmd.Parameters.AddWithValue("@razaoSocial", c.razaoSocial);
                cmd.Parameters.AddWithNullValue("@nomeFantasia", c.nomeFantasia);
                cmd.Parameters.AddWithValue("@cnpj", c.cnpj);
                cmd.Parameters.AddWithNullValue("@inscricaoEstadual", c.inscricaoEstadual);
                cmd.Parameters.AddWithNullValue("inscricaoMunicipal", c.inscricaoMunicipal);
                cmd.Parameters.AddWithValue("@classe", c.classe);
                cmd.Parameters.AddWithValue("idRepresentante", c.representante.idRepresentante);
                cmd.Parameters.AddWithValue("@ativo", c.ativo);
                cmd.Parameters.AddWithValue("@dataCadastro", DateTime.Now);
                c.idCliente = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var e in c.endereco)
                {
                    query = "insert into Endereco (logradouro,numero,complemento,bairro,municipio,uf,cep,telefone1,telefone2,email,tipo,dataCadastro,idCliente) " +
                        "values (@logradouro,@numero,@complemento,@bairro,@municipio,@uf,@cep,@telefone1,@telefone2,@email,@tipo,@dataCadastro,@idCliente)";
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
                    cmd.Parameters.AddWithValue("@dataCadastro", DateTime.Now);
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

        public List<Cliente> ObterClientes(int codCliente, int codun, string razaoSocial, string nomeFantasia, string cnpj,
            int idRepresentante, DateTime dataInico, DateTime dataFim)
        {
            try
            {
                AbrirConexao();

                string query = "select idCliente, ISNULL(codCliente,0) codCliente, codun, razaoSocial, nomeFantasia, cnpj, inscricaoEstadual, " +
                    "inscricaoMunicipal, classe, c.dataCadastro, c.idRepresentante, r.nome from Cliente c " +
                    "inner join Representante r on c.idRepresentante = r.idRepresentante where c.dataCadastro >= @dataInico and c.dataCadastro <= @dataFim ";

                if (dataInico == DateTime.MinValue)
                    dataInico = (DateTime)SqlDateTime.MinValue;

                if (codCliente != 0)
                    query += "and codCliente = @codCliente ";

                if (codun != 0)
                    query += "and codun = @codun ";

                if (razaoSocial != null)
                    query += "and razaoSocial like '%"+@razaoSocial+"%' ";
                
                if (nomeFantasia != null)
                    query += "and nomeFantasia like '%"+@nomeFantasia+"%' ";
                
                if (cnpj != null)
                    query += "and cnpj = @cnpj ";
                                
                if (idRepresentante != 0)
                    query += "and c.idRepresentante = @idRepresentante ";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithNullValue("@codCliente", codCliente);
                cmd.Parameters.AddWithNullValue("@codun", codun);
                cmd.Parameters.AddWithNullValue("@razaoSocial", razaoSocial);
                cmd.Parameters.AddWithNullValue("@nomeFantasia", nomeFantasia);
                cmd.Parameters.AddWithNullValue("@cnpj", cnpj);
                cmd.Parameters.AddWithNullValue("@idRepresentante", idRepresentante);
                cmd.Parameters.AddWithNullValue("@ativo", true);
                cmd.Parameters.AddWithNullValue("@dataInico", dataInico);
                cmd.Parameters.AddWithNullValue("@dataFim", dataFim);
                dr = cmd.ExecuteReader();

                var lista = new List<Cliente>();

                while (dr.Read())
                {
                    var c = new Cliente();
                    c.representante = new Representante();

                    c.idCliente = (int)dr["idCliente"];
                    c.codCliente = (int)dr["codCliente"];
                    c.razaoSocial = dr["razaoSocial"].ToString();
                    c.nomeFantasia = dr["nomeFantasia"].ToString();
                    c.cnpj = dr["cnpj"].ToString();
                    c.inscricaoEstadual = dr["inscricaoEstadual"].ToString();
                    c.inscricaoMunicipal = dr["inscricaoMunicipal"].ToString();
                    c.classe = dr["classe"].ToString();
                    c.dataCadastro = (DateTime)dr["dataCadastro"];
                    c.representante.idRepresentante = (int)dr["idRepresentante"];
                    c.representante.nome = dr["nome"].ToString();

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


        public Cliente ObterCliente(int id)
        {
            try
            {
                AbrirConexao();

                string query = "select idCliente, ISNULL(codCliente,0) codCliente, codun, razaoSocial, nomeFantasia, cnpj, inscricaoEstadual, " +
                    "inscricaoMunicipal, classe, c.dataCadastro, c.idRepresentante, r.nome from Cliente c " +
                    "inner join Representante r on c.idRepresentante = r.idRepresentante where c.idCliente = @idcliente";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idCliente",id);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    var c = new Cliente();
                    c.representante = new Representante();

                    c.idCliente = (int)dr["idCliente"];
                    c.codCliente = (int)dr["codCliente"];
                    c.razaoSocial = dr["razaoSocial"].ToString();
                    c.nomeFantasia = dr["nomeFantasia"].ToString();
                    c.cnpj = dr["cnpj"].ToString();
                    c.inscricaoEstadual = dr["inscricaoEstadual"].ToString();
                    c.inscricaoMunicipal = dr["inscricaoMunicipal"].ToString();
                    c.classe = dr["classe"].ToString();
                    c.dataCadastro = (DateTime)dr["dataCadastro"];
                    c.representante.idRepresentante = (int)dr["idRepresentante"];
                    c.representante.nome = dr["nome"].ToString();

                    return c;
                }
                else
                {
                    throw new Exception("Cod Cliente não encontrado");
                }
                
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

        public List<Endereco> ObterEndereco(int idCliente)
        {
            try
            {
                AbrirConexao();

                string query = "select idEndereco, logradouro, numero, complemento, bairro, municipio, " +
                        "uf, cep, telefone1, telefone2, email, tipo FROM Endereco where idCliente = @idCliente";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                dr = cmd.ExecuteReader();

                var lista = new List<Endereco>();

                while (dr.Read())
                {
                    var e = new Endereco();
                    e.idEndereco = (int)dr["idEndereco"];
                    e.logradouro = dr["logradouro"].ToString();
                    e.numero = dr["numero"].ToString();
                    e.complemento = dr["complemento"].ToString();
                    e.bairro = dr["bairro"].ToString();
                    e.municipio = dr["municipio"].ToString();
                    e.UF = dr["uf"].ToString();
                    e.cep = dr["cep"].ToString();
                    e.telefone1 = dr["telefone1"].ToString();
                    e.telefone2 = dr["telefone2"].ToString();
                    e.email = dr["email"].ToString();
                    e.tipo = dr["tipo"].ToString();

                    lista.Add(e);
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
