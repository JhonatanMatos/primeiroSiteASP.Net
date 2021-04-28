using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Trab_LP_BD_v1.Models;

namespace Trab_LP_BD_v1.DAO
{
    public class CategoriaDAO : PadraoDAO<CategoriaViewModel>
    {
        protected override SqlParameter[] CriaParametros(CategoriaViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("descricao", model.Descricao);
            return parametros;
        }

        protected override CategoriaViewModel MontaModel(DataRow registro)
        {
            CategoriaViewModel g = new CategoriaViewModel();
            g.Id = Convert.ToInt32(registro["id"]);
            g.Descricao = registro["descricao"].ToString();            
            return g;
        }

        protected override void SetTabela()
        {
            Tabela = "categoria";
        }

        public List<CategoriaViewModel> ListaCategorias()
        {
            List<CategoriaViewModel> lista = new List<CategoriaViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemCategorias", null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }
    }
}
