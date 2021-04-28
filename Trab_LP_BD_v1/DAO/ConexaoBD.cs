using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trab_LP_BD_v1.DAO
{
    public static class ConexaoBD
    {        
        public static SqlConnection GetConexao()
        {
            //string strCon = "Data Source=LOCALHOST;Initial Catalog=Teste;user id=sa; password=123456";
            //string strCon = "Data Source=LOCALHOST;Initial Catalog=Teste;Integrated Security=SSPI";
            string strCon = "Data Source=LOCALHOST;Initial Catalog=Teste;Integrated Security=SSPI";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
