using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Trab_LP_BD_v1.Models
{
    public class ProdutosViewModel : PadraoViewModel
    {
        public string Descricao { get; set; }
        public int CategoriaId { get; set; }
        public double Preco { get; set; }
        public IFormFile Imagem { get; set; }
        public string ImageBase64 { get; set; }
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
    }
}
