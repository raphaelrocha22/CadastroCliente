using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Validacao
{
    public class UpdateImageValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value is HttpPostedFileBase)
                {
                    var upload = (HttpPostedFileBase)value;
                    string nomeArquivo = upload.FileName.ToLower();
                    int tamanhoArquivo = upload.ContentLength;

                    return (nomeArquivo.EndsWith("jpg") || nomeArquivo.EndsWith("gif")
                        || nomeArquivo.EndsWith("png") || nomeArquivo.EndsWith("pdf")
                        || nomeArquivo.EndsWith("bmp")) && tamanhoArquivo <= Math.Pow(1024, 2) * 10;
                }
                return false;
            }
            return true;
        }
    }
}