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

            string query = "select idUsuario,nome,login from Usuario where login = @login and senha = @senha";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@login", login);
            string tt = Criptografia.EncriptarSenha(senha);
            cmd.Parameters.AddWithValue("@senha",Criptografia.EncriptarSenha(senha));
            dr = cmd.ExecuteReader();

            Usuario u = null;

            if (dr.Read())
            {
                u = new Usuario();
                u.IdUsuario = (int)dr["idUsuario"];
                u.Nome = (string)dr["nome"];
                u.Login = (string)dr["login"];
            }
            FecharConexao();
            return u;
        }

        public void AtualizarSenha(string senhaNova, int id)
        {
            AbrirConexao();

            string query = "update Usuario set senha = @senhaNova where idUsuario = @id";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@senhaNova",Criptografia.EncriptarSenha(senhaNova));
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            FecharConexao();
        }
    }
}
