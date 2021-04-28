using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Trab_LP_BD_v1.Models;

namespace Trab_LP_BD_v1.DAO
{
    public class LocalDAO : PadraoDAO<LocalViewModel>
    {
        protected override SqlParameter[] CriaParametros(LocalViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("endereco", model.Endereco);            

            return parametros;
        }

        protected override LocalViewModel MontaModel(DataRow registro)
        {
            LocalViewModel l = new LocalViewModel();
            l.Id = Convert.ToInt32(registro["id"]);
            l.Endereco = registro["endereco"].ToString();            
            return l;
        }

        protected override void SetTabela()
        {
            Tabela = "local";
        }
    }
}
