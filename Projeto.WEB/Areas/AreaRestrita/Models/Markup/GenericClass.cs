using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Markup
{
    public class GenericClass
    {
        public ModalidadeMarkup ModalidadeMarkup { get; set; }
        public decimal MediaHistorica { get; set; }
        public decimal MinimoMensalPeriodo { get; set; }

        public int PrazoPagamentoValue { get; set; }
        public string PrazoPagamentoText { get; set; }

        public static List<GenericClass> Modalidade_MediaMinima_MetaMinima()
        {
            var lista = new List<GenericClass>();
            lista.Add(new GenericClass
            {
                ModalidadeMarkup = ModalidadeMarkup.MarkUp4_3,
                MediaHistorica = 1000,
                MinimoMensalPeriodo = 5000
            });
            lista.Add(new GenericClass
            {
                ModalidadeMarkup = ModalidadeMarkup.MarkUp4_8,
                MediaHistorica = 5000,
                MinimoMensalPeriodo = 7500
            });
            lista.Add(new GenericClass
            {
                ModalidadeMarkup = ModalidadeMarkup.MarkUp5,
                MediaHistorica = 5000,
                MinimoMensalPeriodo = 10000
            });
            return lista;
        }
    }
}