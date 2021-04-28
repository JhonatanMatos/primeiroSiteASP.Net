using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trab_LP_BD_v1.Models;
using Trab_LP_BD_v1.DAO;
using System.Data;
using System.Data.SqlClient;

namespace Trab_LP_BD_v1.DAO
{
    public class PedidoDAO : PadraoDAO<PedidoViewModel>
    {
        protected override SqlParameter[] CriaParametros(PedidoViewModel model)
        {
            SqlParameter[] parametros =
            {
                //new SqlParameter("id", model.Id),
                new SqlParameter("data", model.Data)
            };
            return parametros;
        }
        protected override PedidoViewModel MontaModel(DataRow registro)
        {
            PedidoViewModel c = new PedidoViewModel()
            {
                Id = Convert.ToInt32(registro["id"]),
                Data = Convert.ToDateTime(registro["data"])
            };
            return c;
        }


        protected override void SetTabela()
        {
            Tabela = "Pedido";
            ChaveIdentity = true;
        }
    }
}
