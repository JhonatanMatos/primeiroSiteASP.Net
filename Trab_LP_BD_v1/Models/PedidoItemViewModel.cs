using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trab_LP_BD_v1.Models
{
    public class PedidoItemViewModel : PadraoViewModel
    {
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Qtde { get; set; }
        public double Valor { get; set; }
    }
}
