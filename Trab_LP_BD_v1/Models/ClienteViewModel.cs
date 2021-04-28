using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Trab_LP_BD_v1.Models
{
    public class ClienteViewModel : PadraoViewModel
    {
        public string Nome { get; set; }
        public DateTime Nasc { get; set; }
        public char Sexo { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public IFormFile Imagem { get; set; }
        public string ImageBase64 { get; set; }

        public byte[] ImageByte()
        {
            if (Imagem != null)
                using (var ms = new MemoryStream())
                {
                    Imagem.CopyTo(ms);
                    return ms.ToArray();
                }
            else
                return null;
        }

        public byte[] ImagemEmByte { get; set; }        
        public string ImagemEmBase64
        {
            get
            {
                if (ImagemEmByte != null)
                    return Convert.ToBase64String(ImagemEmByte);
                else
                    return string.Empty;
            }
        }


    }
}
