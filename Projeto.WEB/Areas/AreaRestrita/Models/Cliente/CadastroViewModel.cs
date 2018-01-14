using Newtonsoft.Json;
using Projeto.DAL.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class CadastroViewModel:GenericClass
    {
        public string status { get; set; }
    }
}