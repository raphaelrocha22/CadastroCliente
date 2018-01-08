using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Areas.AreaRestrita.Models.JsonClass
{
    public class Modelo
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}