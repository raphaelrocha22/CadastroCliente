using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Areas.AreaRestrita.Models.JsonClass
{
    public class Qsa
    {
        [JsonProperty("qual")]
        public string Qual { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}