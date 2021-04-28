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
    public class LoginDAO : PadraoDAO<LoginViewModel>
    {
        protected override SqlParameter[] CriaParametros(LoginViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("usuario", model.Usuario);
            parametros[1] = new SqlParameter("senha", model.Senha);

            return parametros;
        }

        protected override LoginViewModel MontaModel(DataRow registro)
        {
            LoginViewModel l = new LoginViewModel();
            l.Usuario = registro["usuario"].ToString();
            l.Senha = registro["senha"].ToString();

            return l;
        }

        protected override void SetTabela()
        {
            Tabela = "Login";
        }
        public LoginViewModel Consulta_Login(string Usuario)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("Usuario",Usuario )
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsulta_Login", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }
    }
}
