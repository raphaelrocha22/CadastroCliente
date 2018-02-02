﻿using Projeto.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Models.ClubR
{
    public class GenericClass
    {
        public ModalidadeClubR ModalidadeClubR { get; set; }
        public string PrazoContrato { get; set; }
        public int PeriodoMeses { get; set; }
        public string PrazoPagamento { get; set; }
        public decimal Markup { get; set; }
        public decimal crescimentoMinimo { get; set; }
        public decimal crescimentoMaximo { get; set; }
        public decimal RebatePercent { get; set; }
        public decimal MediaHistorica { get; set; }
        public decimal MinimoMensalPeriodo { get; set; }

        public static List<GenericClass> Modalidade_PrazoContrato()
        {
            var lista = new List<GenericClass>();

            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Ouro"),
                PrazoContrato = "Trimestral",
                PeriodoMeses = 3
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Diamante"),
                PrazoContrato = "Semestral",
                PeriodoMeses = 6
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Diamante"),
                PrazoContrato = "Anual",
                PeriodoMeses = 12
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Platina"),
                PrazoContrato = "Anual",
                PeriodoMeses = 12
            });

            return lista;
        }
        
        public static List<GenericClass> Modalidade_MediaMinima_MetaMinima()
        {
            var lista = new List<GenericClass>();
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Ouro"),
                MediaHistorica = 10000,
                MinimoMensalPeriodo = 12500
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Diamante"),
                MediaHistorica = 20000,
                MinimoMensalPeriodo = 23000
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Platina"),
                MediaHistorica = 50000,
                MinimoMensalPeriodo = 55000
            });
            return lista;
        }
    }
}