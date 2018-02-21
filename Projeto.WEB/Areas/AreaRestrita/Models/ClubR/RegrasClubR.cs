using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Models.ClubR
{
    public class RegrasClubR
    {
        public ModalidadeClubR ModalidadeClubR { get; set; }
        public string PrazoContrato { get; set; }
        public int PeriodoMeses { get; set; }
        public decimal MediaHistorica { get; set; }
        public decimal MinimoMensalPeriodo { get; set; }

        public static List<RegrasClubR> Modalidade_PrazoContrato()
        {
            var lista = new List<RegrasClubR>();

            lista.Add(new RegrasClubR
            {
                ModalidadeClubR = ModalidadeClubR.Ouro,
                PrazoContrato = "Trimestral",
                PeriodoMeses = 3
            });
            lista.Add(new RegrasClubR
            {
                ModalidadeClubR = ModalidadeClubR.Diamante,
                PrazoContrato = "Semestral",
                PeriodoMeses = 6
            });
            lista.Add(new RegrasClubR
            {
                ModalidadeClubR = ModalidadeClubR.Diamante,
                PrazoContrato = "Anual",
                PeriodoMeses = 12
            });
            lista.Add(new RegrasClubR
            {
                ModalidadeClubR = ModalidadeClubR.Platina,
                PrazoContrato = "Anual",
                PeriodoMeses = 12
            });

            return lista;
        }
        
        public static List<RegrasClubR> Modalidade_MediaMinima_MetaMinima()
        {
            var lista = new List<RegrasClubR>();
            lista.Add(new RegrasClubR
            {
                ModalidadeClubR = ModalidadeClubR.Ouro,
                MediaHistorica = 10000,
                MinimoMensalPeriodo = 12500
            });
            lista.Add(new RegrasClubR
            {
                ModalidadeClubR = ModalidadeClubR.Diamante,
                MediaHistorica = 20000,
                MinimoMensalPeriodo = 23000
            });
            lista.Add(new RegrasClubR
            {
                ModalidadeClubR = ModalidadeClubR.Platina,
                MediaHistorica = 50000,
                MinimoMensalPeriodo = 55000
            });
            return lista;
        }
    }
}