﻿using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public class Representante
    {
        public int IdRepresentante { get; set; }
        public string Nome { get; set; }
        public string TipoRepresentante { get; set; }
        public bool Ativo { get; set; }
    }
}
