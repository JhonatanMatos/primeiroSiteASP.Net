using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Trab_LP_BD_v1.Models;

namespace Trab_LP_BD_v1.DAO
{
    public class FuncionarioDAO : PadraoDAO<FuncionarioViewModel>
    {
        protected override SqlParameter[] CriaParametros(FuncionarioViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("nome", model.Nome);            

            return parametros;
        }

        protected override FuncionarioViewModel MontaModel(DataRow registro)
        {
            FuncionarioViewModel f = new FuncionarioViewModel();
            f.Id = Convert.ToInt32(registro["id"]);
            f.Nome = registro["nome"].ToString();            
            
            return f;
        }

        protected override void SetTabela()
        {
            Tabela = "funcionarios";
        }
    }
}
