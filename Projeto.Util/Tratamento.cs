using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Util
{
    public class Tratamento
    {
        public static string NullToString(string str)
        {
            return string.IsNullOrEmpty(str) ? string.Empty : str;
        }        
    }
}
