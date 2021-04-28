using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trab_LP_BD_v1.Models
{
    public abstract class PadraoViewModel
    {
        public virtual int? Id { get; set; }
        public string Tela { get; set; }
    }
}
