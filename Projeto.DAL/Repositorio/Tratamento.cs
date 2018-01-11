using System;
using System.Data.SqlClient;

namespace Projeto.DAL.Repositorio
{
    public static class Tratamento
    {
        public static SqlParameter AddWithNullValue(this SqlParameterCollection collection, string parameterName, object value)
        {
            return value == null ? collection.AddWithValue(parameterName, DBNull.Value) : collection.AddWithValue(parameterName, value);
        }
    }
}
