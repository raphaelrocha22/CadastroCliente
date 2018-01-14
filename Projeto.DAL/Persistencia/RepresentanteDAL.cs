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
                    r.idRepresentante = (int)dr["idRepresentante"];
                    r.nome = (string)dr["nome"];
                    r.tipoRepresentante = (string)dr["tipoRepresentante"];

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
    }
}
