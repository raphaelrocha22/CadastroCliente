using Projeto.DAL.Repositorio;
using Projeto.Entidades;
using Projeto.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Persistencia
{
    public class UsuarioDAL:Conexao
    {
        public Usuario ObterUsuarioSenha(string login, string senha)
        {
            AbrirConexao();

            string query = "select id,nome,login,ativo from usuario where login = @login and senha = @senha and ativo = 1";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@senha",Criptografia.EncriptarSenha(senha));
            dr = cmd.ExecuteReader();

            Usuario u = null;

            if (dr.Read())
            {
                u = new Usuario();
                u.idUsuario = (int)dr["id"];
                u.nome = (string)dr["nome"];
                u.login = (string)dr["login"];
                u.ativo = (bool)dr["ativo"];
            }
            
            FecharConexao();
            return u;
        }

        public void AtualizarSenha(string senhaNova, int id)
        {
            AbrirConexao();

            string query = "update Usuario set senha = @senhaNova where id = @id";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@senhaNova",Criptografia.EncriptarSenha(senhaNova));
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            FecharConexao();
        }
    }
}
