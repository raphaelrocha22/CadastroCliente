using Projeto.DAL.Repositorio;
using Projeto.Entidades;
using Projeto.Entidades.Enum;
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
                cmd.Parameters.AddWithNullValue("@codun", c.Codun);
                cmd.Parameters.AddWithValue("@razaoSocial", c.RazaoSocial);
                cmd.Parameters.AddWithNullValue("@nomeFantasia", c.NomeFantasia);
                cmd.Parameters.AddWithValue("@cnpj", c.Cnpj);
                cmd.Parameters.AddWithNullValue("@inscricaoEstadual", c.InscricaoEstadual);
                cmd.Parameters.AddWithNullValue("inscricaoMunicipal", c.InscricaoMunicipal);
                cmd.Parameters.AddWithValue("@classe", c.Classe.ToString());
                cmd.Parameters.AddWithValue("idRepresentante", c.Representante.IdRepresentante);
                cmd.Parameters.AddWithValue("@ativo", c.Ativo);
                cmd.Parameters.AddWithValue("@dataCadastro", DateTime.Now);
                c.IdCliente = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var e in c.Enderecos)
                {
                    query = "insert into Endereco (logradouro,numero,complemento,bairro,municipio,uf,cep,telefone1,telefone2,email,tipo,dataCadastro,idCliente) " +
                        "values (@logradouro,@numero,@complemento,@bairro,@municipio,@uf,@cep,@telefone1,@telefone2,@email,@tipo,@dataCadastro,@idCliente)";
                    cmd = new SqlCommand(query, con, tr);
                    cmd.Parameters.AddWithValue("@logradouro", e.Logradouro);
                    cmd.Parameters.AddWithValue("@numero", e.Numero);
                    cmd.Parameters.AddWithNullValue("@complemento", e.Complemento);
                    cmd.Parameters.AddWithValue("@bairro", e.Bairro);
                    cmd.Parameters.AddWithValue("@municipio", e.Municipio);
                    cmd.Parameters.AddWithValue("@uf", e.UF);
                    cmd.Parameters.AddWithValue("@cep", e.Cep);
                    cmd.Parameters.AddWithNullValue("@telefone1", e.Telefone1);
                    cmd.Parameters.AddWithNullValue("@telefone2", e.Telefone2);
                    cmd.Parameters.AddWithNullValue("@email", e.Email);
                    cmd.Parameters.AddWithValue("@tipo", e.Tipo.ToString());
                    cmd.Parameters.AddWithValue("@idCliente", c.IdCliente);
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

        public List<Cliente> ObterClientes(int idCliente = 0, int codCliente = 0, int codun = 0, string razaoSocial = null, string nomeFantasia = null, string cnpj = null,
            int idRepresentante = 0, DateTime ? dataInico = null, DateTime ? dataFim = null)
        {
            try
            {
                AbrirConexao();

                string query = "select idCliente, ISNULL(codCliente,0) codCliente, codun, razaoSocial, nomeFantasia, cnpj, inscricaoEstadual, " +
                    "inscricaoMunicipal, classe, c.dataCadastro, c.idRepresentante, r.nome from Cliente c " +
                    "inner join Representante r on c.idRepresentante = r.idRepresentante where c.dataCadastro >= @dataInico and c.dataCadastro <= @dataFim ";

                if (dataInico == DateTime.MinValue || dataInico == null)
                    dataInico = (DateTime)SqlDateTime.MinValue;

                if (dataFim == DateTime.MinValue || dataFim == null)
                    dataFim = (DateTime)SqlDateTime.MaxValue;

                if (idCliente != 0)
                    query += "and idCliente = @idCliente ";

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

                /// Implementar a pesquisa por Estado, Cidade e Bairro

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithNullValue("@idCliente", idCliente);
                cmd.Parameters.AddWithNullValue("@codCliente", codCliente);
                cmd.Parameters.AddWithNullValue("@codun", codun);
                cmd.Parameters.AddWithNullValue("@razaoSocial", razaoSocial);
                cmd.Parameters.AddWithNullValue("@nomeFantasia", nomeFantasia);
                cmd.Parameters.AddWithNullValue("@cnpj", cnpj);
                cmd.Parameters.AddWithNullValue("@idRepresentante", idRepresentante);
                cmd.Parameters.AddWithNullValue("@dataInico", dataInico);
                cmd.Parameters.AddWithNullValue("@dataFim", dataFim);
                dr = cmd.ExecuteReader();

                var lista = new List<Cliente>();

                while (dr.Read())
                {
                    var c = new Cliente();
                    c.Representante = new Representante();

                    c.IdCliente = (int)dr["idCliente"];
                    c.CodCliente = (int)dr["codCliente"];
                    c.Codun = (int)dr["codun"];
                    c.RazaoSocial = dr["razaoSocial"].ToString();
                    c.NomeFantasia = dr["nomeFantasia"].ToString();
                    c.Cnpj = dr["cnpj"].ToString();
                    c.InscricaoEstadual = dr["inscricaoEstadual"].ToString();
                    c.InscricaoMunicipal = dr["inscricaoMunicipal"].ToString();
                    c.Classe =(ClasseCliente)Enum.Parse(typeof(ClasseCliente),dr["classe"].ToString());
                    c.DataCadastro = (DateTime)dr["dataCadastro"];
                    c.Representante.IdRepresentante = (int)dr["idRepresentante"];
                    c.Representante.Nome = dr["nome"].ToString();

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

        public List<Endereco> ObterEndereco(int idCliente)
        {
            try
            {
                AbrirConexao();

                string query = "select idEndereco, logradouro, numero, complemento, bairro, municipio, " +
                        "uf, cep, telefone1, telefone2, email, tipo, dataCadastro FROM Endereco where idCliente = @idCliente";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                dr = cmd.ExecuteReader();

                var lista = new List<Endereco>();

                while (dr.Read())
                {
                    var e = new Endereco();
                    e.IdEndereco = (int)dr["idEndereco"];
                    e.Logradouro = dr["logradouro"].ToString();
                    e.Numero = dr["numero"].ToString();
                    e.Complemento = dr["complemento"].ToString();
                    e.Bairro = dr["bairro"].ToString();
                    e.Municipio = dr["municipio"].ToString();
                    e.UF = dr["uf"].ToString();
                    e.Cep = dr["cep"].ToString();
                    e.Telefone1 = dr["telefone1"].ToString();
                    e.Telefone2 = dr["telefone2"].ToString();
                    e.Email = dr["email"].ToString();
                    e.Tipo = (TipoEndereco)Enum.Parse(typeof(TipoEndereco),dr["tipo"].ToString());
                    e.DataCadastro = (DateTime)dr["dataCadastro"];

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
                    "inscricaoMunicipal = @inscricaoMunicipal, classe = @classe, idRepresentante = @idRepresentante, " +
                    "dataCadastro = @dataCadastro where idCliente = @idCliente";
                cmd = new SqlCommand(query, con, tr);
                cmd.Parameters.AddWithNullValue("@codCliente", c.CodCliente);
                cmd.Parameters.AddWithNullValue("@codun", c.Codun);
                cmd.Parameters.AddWithValue("@razaoSocial", c.RazaoSocial);
                cmd.Parameters.AddWithNullValue("@nomeFantasia", c.NomeFantasia);
                cmd.Parameters.AddWithValue("@cnpj", c.Cnpj);
                cmd.Parameters.AddWithNullValue("@inscricaoEstadual", c.InscricaoEstadual);
                cmd.Parameters.AddWithNullValue("@inscricaoMunicipal", c.InscricaoMunicipal);
                cmd.Parameters.AddWithValue("@classe", c.Classe.ToString());
                cmd.Parameters.AddWithValue("@idRepresentante", c.Representante.IdRepresentante);
                cmd.Parameters.AddWithValue("@dataCadastro", DateTime.Now);
                cmd.Parameters.AddWithValue("@idCliente", c.IdCliente);
                cmd.ExecuteNonQuery();

                foreach (var e in c.Enderecos)
                {
                    query = "update Endereco set logradouro = @logradouro, numero = @numero, complemento = @complemento, " +
                        "bairro = @bairro, municipio = @municipio, uf = @uf, cep = @cep, telefone1 = @telefone1, " +
                        "telefone2 = @telefone2, email = @email, tipo = @tipo, dataCadastro = @dataCadastro " +
                        "where idEndereco = @idEndereco and idCliente = @idCliente";
                    cmd = new SqlCommand(query, con, tr);
                    cmd.Parameters.AddWithValue("@logradouro", e.Logradouro);
                    cmd.Parameters.AddWithValue("@numero", e.Numero);
                    cmd.Parameters.AddWithNullValue("@complemento", e.Complemento);
                    cmd.Parameters.AddWithValue("@bairro", e.Bairro);
                    cmd.Parameters.AddWithValue("@municipio", e.Municipio);
                    cmd.Parameters.AddWithValue("@uf", e.UF);
                    cmd.Parameters.AddWithValue("@cep", e.Cep);
                    cmd.Parameters.AddWithNullValue("@telefone1", e.Telefone1);
                    cmd.Parameters.AddWithNullValue("@telefone2", e.Telefone2);
                    cmd.Parameters.AddWithNullValue("@email", e.Email);
                    cmd.Parameters.AddWithValue("@tipo", e.Tipo.ToString());
                    cmd.Parameters.AddWithValue("@dataCadastro", DateTime.Now);
                    cmd.Parameters.AddWithValue("@idCliente", c.IdCliente);
                    cmd.Parameters.AddWithValue("@idEndereco", e.IdEndereco);
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
