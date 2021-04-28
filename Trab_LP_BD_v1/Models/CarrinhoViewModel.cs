using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trab_LP_BD_v1.Models
{
    public class CarrinhoViewModel : PadraoViewModel
    {
        public int Quantidade { get; set; }
        public int ProdutoId { get; set; }
        public double Valor { get; set; }

        public string Nome { get; set; }

        public string ImagemEmBase64 { get; set; }
    }
}
