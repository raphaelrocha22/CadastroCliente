using Projeto.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Repositorio
{
    public class Conexao
    {
        protected SqlConnection con;
        protected SqlCommand cmd;
        protected SqlTransaction tr;
        protected SqlDataReader dr;

        protected void AbrirConexao()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["azureDB"].ConnectionString);
            con.Open();
        }

        protected void FecharConexao()
        {
            con.Close();
        }

        
    }
}
