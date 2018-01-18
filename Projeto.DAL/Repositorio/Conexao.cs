using System.Configuration;
using System.Data.SqlClient;

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
