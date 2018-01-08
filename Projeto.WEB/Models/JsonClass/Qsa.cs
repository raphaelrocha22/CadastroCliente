using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.WEB.Models.JsonClass
{
    public class Qsa
    {
        [JsonProperty("qual")]
        public string Qual { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}
