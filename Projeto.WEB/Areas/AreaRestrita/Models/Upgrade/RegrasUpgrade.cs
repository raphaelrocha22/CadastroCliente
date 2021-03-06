﻿using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Upgrade
{
    public class RegrasUpgrade
    {
        public ModalidadeUpgrade ModalidadeUpgrade { get; set; }
        public decimal MediaHistorica { get; set; }
        public decimal MinimoMensalPeriodo { get; set; }

        public static List<RegrasUpgrade> Modalidade_MediaMinima_MetaMinima()
        {
            var lista = new List<RegrasUpgrade>();
            lista.Add(new RegrasUpgrade
            {
                ModalidadeUpgrade = ModalidadeUpgrade.Modalidade1,
                MediaHistorica = 1000,
                MinimoMensalPeriodo = 5000
            });
            lista.Add(new RegrasUpgrade
            {
                ModalidadeUpgrade = ModalidadeUpgrade.Modalidade2,
                MediaHistorica = 5000,
                MinimoMensalPeriodo = 7500
            });
            lista.Add(new RegrasUpgrade
            {
                ModalidadeUpgrade = ModalidadeUpgrade.Modalidade3,
                MediaHistorica = 5000,
                MinimoMensalPeriodo = 10000
            });
            return lista;
        }
    }
}