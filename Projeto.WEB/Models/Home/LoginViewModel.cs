using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Models.Home
{
    public class LoginViewModel
    {
        [RegularExpression("^[a-z0-9]{4,50}$", ErrorMessage = "Login inválido")]
        [Required(ErrorMessage = "Por favor, informe o Login do usuário.")]
        [Display(Name = "Login")]
        public string login { get; set; }

        [RegularExpression("^[a-z0-9@&]{4,50}$", ErrorMessage = "Senha inválida")]
        [Required(ErrorMessage = "Por favor, informe a Senha Antiga.")]
        [Display(Name = "Senha")]
        public string senha { get; set; }
    }
}