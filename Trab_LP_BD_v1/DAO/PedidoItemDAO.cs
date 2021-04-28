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
    public class PedidoItemDAO : PadraoDAO<PedidoItemViewModel>
    {
        protected override SqlParameter[] CriaParametros(PedidoItemViewModel model)
        {
            SqlParameter[] parametros =
             {
                //new SqlParameter("id", model.Id),
                new SqlParameter("PedidoId", model.PedidoId),
                new SqlParameter("ProdutoId", model.ProdutoId),
                new SqlParameter("Qtde", model.Qtde),
                new SqlParameter("Valor", model.Valor)
             };
            return parametros;
        }

        protected override PedidoItemViewModel MontaModel(DataRow registro)
        {
            PedidoItemViewModel c = new PedidoItemViewModel()
            {
                Id = Convert.ToInt32(registro["id"]),
                ProdutoId = Convert.ToInt32(registro["ProdutoId"]),
                PedidoId = Convert.ToInt32(registro["PedidoId"]),
                Qtde = Convert.ToInt32(registro["id"]),
                Valor = Convert.ToDouble(registro["valor"])
            };
            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "PedidoItem";
            ChaveIdentity = true;

        }
    }
}
