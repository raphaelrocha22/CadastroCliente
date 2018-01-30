using Projeto.Entidades.Enum;
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
        public decimal Desconto { get; set; }
        public decimal Markup { get; set; }
        public decimal crescimentoMinimo { get; set; }
        public decimal crescimentoMaximo { get; set; }
        public decimal RebatePercent { get; set; }
        public decimal MediaHistorica { get; set; }
        public decimal MetaPeriodo { get; set; }

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

        public static List<GenericClass> PrazoPagamento_Desconto()
        {
            var lista = new List<GenericClass>();

            lista.Add(new GenericClass { PrazoPagamento = "2 Vezes", Desconto = 0.40M});
            lista.Add(new GenericClass { PrazoPagamento = "2 Vezes", Desconto = 0.36M});
            lista.Add(new GenericClass { PrazoPagamento = "2 Vezes", Desconto = 0.33M});
            lista.Add(new GenericClass { PrazoPagamento = "2 Vezes", Desconto = 0.30M});
            lista.Add(new GenericClass { PrazoPagamento = "2 Vezes", Desconto = 0.25M});

            lista.Add(new GenericClass { PrazoPagamento = "3 Vezes", Desconto = 0.40M});
            lista.Add(new GenericClass { PrazoPagamento = "3 Vezes", Desconto = 0.36M});
            lista.Add(new GenericClass { PrazoPagamento = "3 Vezes", Desconto = 0.33M});
            lista.Add(new GenericClass { PrazoPagamento = "3 Vezes", Desconto = 0.30M});
            lista.Add(new GenericClass { PrazoPagamento = "3 Vezes", Desconto = 0.25M});

            lista.Add(new GenericClass { PrazoPagamento = "4 Vezes", Desconto = 0.40M});
            lista.Add(new GenericClass { PrazoPagamento = "4 Vezes", Desconto = 0.36M});
            lista.Add(new GenericClass { PrazoPagamento = "4 Vezes", Desconto = 0.33M});
            lista.Add(new GenericClass { PrazoPagamento = "4 Vezes", Desconto = 0.30M});
            lista.Add(new GenericClass { PrazoPagamento = "4 Vezes", Desconto = 0.25M});

            lista.Add(new GenericClass { PrazoPagamento = "30 Dias", Desconto = 0.425M});
            lista.Add(new GenericClass { PrazoPagamento = "30 Dias", Desconto = 0.385M});
            lista.Add(new GenericClass { PrazoPagamento = "30 Dias", Desconto = 0.355M});
            lista.Add(new GenericClass { PrazoPagamento = "30 Dias", Desconto = 0.325M});
            lista.Add(new GenericClass { PrazoPagamento = "30 Dias", Desconto = 0.275M});

            lista.Add(new GenericClass { PrazoPagamento = "9 Vezes", Desconto = 0.36M});
            lista.Add(new GenericClass { PrazoPagamento = "9 Vezes", Desconto = 0.32M});
            lista.Add(new GenericClass { PrazoPagamento = "9 Vezes", Desconto = 0.29M});
            lista.Add(new GenericClass { PrazoPagamento = "9 Vezes", Desconto = 0.26M});
            lista.Add(new GenericClass { PrazoPagamento = "9 Vezes", Desconto = 0.21M});

            return lista;
        }
        
        public static List<GenericClass> Modalidade_MediaMinima_MetaMinima()
        {
            var lista = new List<GenericClass>();
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Ouro"),
                MediaHistorica = 10000,
                MetaPeriodo = 37500
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Diamante"),
                MediaHistorica = 20000,
                MetaPeriodo = 138000
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Platina"),
                MediaHistorica = 50000,
                MetaPeriodo = 660000
            });
            return lista;
        }

    }
}