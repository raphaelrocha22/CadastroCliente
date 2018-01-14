﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class ConsultaViewModel:GenericClass
    {
        public int idCliente { get; set; }

        public int codCliente { get; set; }

        public DateTime dataInicio { get; set; }

        public DateTime dataFim { get; set; }

        public string dataCadastro { get; set; }

    }
}