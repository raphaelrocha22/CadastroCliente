using Projeto.DAL.Repositorio;
using Projeto.Entidades;
using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Projeto.DAL.Persistencia
{
    public class UsuarioDAL : Conexao
    {
        public void CadastrarUsuario(Usuario u)
        {
            try
            {
                AbrirConexao();

                string query = "insert into Usuario (nome,login,senha,email,perfil,status) values (@nome,@login,@senha,@email,@perfil,@status)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nome", u.Nome);
                cmd.Parameters.AddWithValue("@login", u.Login);
                cmd.Parameters.AddWithValue("@senha", Criptografia.EncriptarSenha(u.Senha));
                cmd.Parameters.AddWithValue("@email", u.Email);
                cmd.Parameters.AddWithValue("@perfil", u.Perfil.ToString());
                cmd.Parameters.AddWithValue("@status", u.Status.ToString());
                cmd.ExecuteNonQuery();
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

        public Usuario ObterUsuario(string login, string senha)
        {
            try
            {
                AbrirConexao();

                string query = "select idUsuario,nome,login,email,perfil,status from Usuario where login = @login and senha = @senha";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@senha", Criptografia.EncriptarSenha(senha));
                dr = cmd.ExecuteReader();

                Usuario u = null;

                if (dr.Read())
                {
                    u = new Usuario();
                    u.IdUsuario = (int)dr["idUsuario"];
                    u.Nome = (string)dr["nome"];
                    u.Login = (string)dr["login"];
                    u.Email = (string)dr["email"];
                    u.Perfil = (PerfilUsuario)Enum.Parse(typeof(PerfilUsuario), dr["perfil"].ToString());
                    u.Status = (StatusUsuario)Enum.Parse(typeof(StatusUsuario), dr["status"].ToString());
                }
                return u;
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

        public Usuario ObterUsuario(int id)
        {
            try
            {
                AbrirConexao();

                string query = "select idUsuario,nome,login,email,perfil,status from Usuario where idUsuario = @id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id",id);
                dr = cmd.ExecuteReader();

                Usuario u = null;

                if (dr.Read())
                {
                    u = new Usuario();
                    u.IdUsuario = (int)dr["idUsuario"];
                    u.Nome = (string)dr["nome"];
                    u.Login = (string)dr["login"];
                    u.Email = (string)dr["email"];
                    u.Perfil = (PerfilUsuario)Enum.Parse(typeof(PerfilUsuario), dr["perfil"].ToString());
                    u.Status = (StatusUsuario)Enum.Parse(typeof(StatusUsuario), dr["status"].ToString());
                }
                return u;
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
                
        public void AtualizarSenha(string senhaNova, int id)
        {
            try
            {
                AbrirConexao();

                string query = "update Usuario set senha = @senhaNova where idUsuario = @id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@senhaNova", Criptografia.EncriptarSenha(senhaNova));
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
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

        public void AtualizarUsuario(Usuario u)
        {
            try
            {
                AbrirConexao();

                string query = "update Usuario set nome = @nome, login = @login, email = @email, status = @status, perfil = @perfil where idUsuario = @id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nome", u.Nome);
                cmd.Parameters.AddWithValue("@login", u.Login);
                cmd.Parameters.AddWithValue("@email", u.Email);
                cmd.Parameters.AddWithValue("@status", u.Status.ToString());
                cmd.Parameters.AddWithValue("@perfil", u.Perfil).ToString();
                cmd.Parameters.AddWithValue("@id", u.IdUsuario);
                cmd.ExecuteNonQuery();
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

        public List<Usuario>ListarUsuarios()
        {
            try
            {
                AbrirConexao();

                string query = "select idUsuario, nome, login, email, perfil, status from Usuario";
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();

                var lista = new List<Usuario>();

                while (dr.Read())
                {
                    var u = new Usuario();
                    u.IdUsuario = (int)dr["idUsuario"];
                    u.Nome = dr["nome"].ToString();
                    u.Login = dr["login"].ToString();
                    u.Email = dr["email"].ToString();
                    u.Perfil = (PerfilUsuario)Enum.Parse(typeof(PerfilUsuario), dr["perfil"].ToString());
                    u.Status = (StatusUsuario)Enum.Parse(typeof(StatusUsuario), dr["status"].ToString());

                    lista.Add(u);
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
