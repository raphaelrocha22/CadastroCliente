using System;
using System.Security.Cryptography;
using System.Text;

namespace Projeto.DAL.Repositorio
{
    public class Criptografia
    {
        public static string EncriptarSenha(string senha)
        {
            var md5 = new MD5CryptoServiceProvider();

            byte[] senhaBinario = Encoding.UTF8.GetBytes(senha);
            byte[] hash = md5.ComputeHash(senhaBinario);

            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
}
