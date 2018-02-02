using Projeto.DAL.Repositorio;
using Projeto.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Projeto.DAL.Persistencia
{
    public class RepresentanteDAL:Conexao
    {
        public List<Representante> ListaRepresentante(int idUsuario)
        {
            try
            {
                AbrirConexao();

                string query = "select r.idRepresentante, r.nome, r.tipoRepresentante from Representante r " +
                    "inner join Representante_Usuario ru on r.idRepresentante = ru.idRepresentante " +
                    "where ru.idUsuario = @idusuario and r.ativo = @ativo";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@ativo", true);
                dr = cmd.ExecuteReader();

                var lista = new List<Representante>();
                while (dr.Read())
                {
                    var r = new Representante();
                    r.IdRepresentante = (int)dr["idRepresentante"];
                    r.Nome = (string)dr["nome"];
                    r.TipoRepresentante = (string)dr["tipoRepresentante"];

                    lista.Add(r);
                }
                FecharConexao();
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

        public List<string> ListaDestinatarios(int idUsuario)
        {
            try
            {
                AbrirConexao();

                string query = "select u.email emailUsuario, rg.email emailGerente from Usuario u " +
                    "inner join Representante_Usuario ru on u.idUsuario = ru.idUsuario " +
                    "inner join Representante r on r.idRepresentante = ru.idRepresentante " +
                    "inner join Representante rg on rg.idRepresentante = r.idGerente " +
                    "where u.idUsuario = @idUsuario";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                dr = cmd.ExecuteReader();

                var lista = new List<string>();

                while (dr.Read())
                {
                    lista.Add(dr["emailUsuario"].ToString());
                    lista.Add(dr["emailGerente"].ToString());
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
