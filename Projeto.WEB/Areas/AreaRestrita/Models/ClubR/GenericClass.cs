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
        public string Programa { get; set; }
        public int NumeroContrato { get; set; }
        public int Codun { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeResponsavel { get; set; }
        public string CpfResponsavel { get; set; }
        public DateTime DataNegociacao { get; set; }
        
        
        public DateTime DataInicioContrato { get; set; }
        public DateTime DataFimContrato { get; set; }
        public decimal MediaHistorica { get; set; }
        public decimal MetaPeriodo { get; set; }
        public string CrescimentoProposto { get; set; }
        public string PrazoPagamento { get; set; }
        public string Desconto { get; set; }
        public string RebatePercent { get; set; }
        public decimal RebateValor { get; set; }
        public bool Ativo { get; set; }

        public int PeriodoMeses { get; set; }
        public ModalidadeClubR ModalidadeClubR { get; set; }
        public decimal crescimentoMinimo { get; set; }
        public decimal crescimentoMaximo { get; set; }
        public string PrazoContrato { get; set; }

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

            lista.Add(new GenericClass { PrazoPagamento = "Normal", Desconto = "0.40" });
            lista.Add(new GenericClass { PrazoPagamento = "Normal", Desconto = "0.3" });
            lista.Add(new GenericClass { PrazoPagamento = "Normal", Desconto = "0.33" });
            lista.Add(new GenericClass { PrazoPagamento = "Normal", Desconto = "0.30" });
            lista.Add(new GenericClass { PrazoPagamento = "Normal", Desconto = "0.25" });

            lista.Add(new GenericClass { PrazoPagamento = "30 Dias", Desconto = "0.425" });
            lista.Add(new GenericClass { PrazoPagamento = "30 Dias", Desconto = "0.39" });
            lista.Add(new GenericClass { PrazoPagamento = "30 Dias", Desconto = "0.35" });
            lista.Add(new GenericClass { PrazoPagamento = "30 Dias", Desconto = "0.33" });
            lista.Add(new GenericClass { PrazoPagamento = "30 Dias", Desconto = "0.275" });

            lista.Add(new GenericClass { PrazoPagamento = "9 Vezes", Desconto = "0.36" });
            lista.Add(new GenericClass { PrazoPagamento = "9 Vezes", Desconto = "0.33" });
            lista.Add(new GenericClass { PrazoPagamento = "9 Vezes", Desconto = "0.30" });
            lista.Add(new GenericClass { PrazoPagamento = "9 Vezes", Desconto = "0.27" });
            lista.Add(new GenericClass { PrazoPagamento = "9 Vezes", Desconto = "0.22" });

            return lista;
        }
        
        public static List<GenericClass> Modalidade_Crescimento_Rebate()
        {
            var lista = new List<GenericClass>();

            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Ouro"),
                crescimentoMinimo = 0.250M,
                crescimentoMaximo = 0.499M,
                RebatePercent = "0.03"
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Ouro"),
                crescimentoMinimo = 0.500M,
                crescimentoMaximo = 0.999M,
                RebatePercent = "0.04"
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Ouro"),
                crescimentoMinimo = 1,
                crescimentoMaximo = 10000,
                RebatePercent = "0.05"
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Diamante"),
                crescimentoMinimo = 0.200M,
                crescimentoMaximo = 0.299M,
                RebatePercent = "0.03"
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Diamante"),
                crescimentoMinimo = 0.300M,
                crescimentoMaximo = 0.499M,
                RebatePercent = "0.04"
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Diamante"),
                crescimentoMinimo = 0.50M,
                crescimentoMaximo = 1000M,
                RebatePercent = "0.05"
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Platina"),
                crescimentoMinimo = 0.100M,
                crescimentoMaximo = 0.149M,
                RebatePercent = "0.03"
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Platina"),
                crescimentoMinimo = 0.150M,
                crescimentoMaximo = 0.299M,
                RebatePercent = "0.04"
            });
            lista.Add(new GenericClass
            {
                ModalidadeClubR = (ModalidadeClubR)Enum.Parse(typeof(ModalidadeClubR), "Platina"),
                crescimentoMinimo = 0.250M,
                crescimentoMaximo = 1000M,
                RebatePercent = "0.05"
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