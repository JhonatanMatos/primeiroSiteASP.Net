using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Trab_LP_BD_v1.DAO;
using Trab_LP_BD_v1.Models;

namespace Trab_LP_BD_v1.DAO
{
    public class ClienteDAO : PadraoDAO<ClienteViewModel>
    {

        ClienteViewModel c;

        protected override SqlParameter[] CriaParametros(ClienteViewModel model)
        {
            object imgByte = model.ImageByte();
            if (imgByte == null)
                imgByte = DBNull.Value;

            SqlParameter[] parametros = new SqlParameter[13];
            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("nome", model.Nome);
            parametros[2] = new SqlParameter("cep", model.CEP);
            parametros[3] = new SqlParameter("endereco", model.Endereco);
            parametros[4] = new SqlParameter("bairro", model.Bairro);
            parametros[5] = new SqlParameter("cidade", model.Cidade);
            parametros[6] = new SqlParameter("estado", model.Estado);
            parametros[7] = new SqlParameter("telefone", model.Telefone);
            parametros[8] = new SqlParameter("email", model.Email);
            parametros[9] = new SqlParameter("senha", model.Senha);
            parametros[10] = new SqlParameter("imagem", imgByte);
            parametros[11] = new SqlParameter("nasc", model.Nasc);
            parametros[12] = new SqlParameter("sexo", model.Sexo);

            return parametros;
        }

        protected override ClienteViewModel MontaModel(DataRow registro)
        {
            c = new ClienteViewModel();
            c.Id = Convert.ToInt32(registro["id"]);
            c.Nome = registro["nome"].ToString();
            c.CEP = registro["cep"].ToString();           
            c.Endereco = registro["endereco"].ToString();
            c.Bairro = registro["bairro"].ToString();
            c.Cidade = registro["cidade"].ToString();
            c.Estado = registro["estado"].ToString();
            c.Telefone = registro["telefone"].ToString();
            c.Email = registro["email"].ToString();
            c.Senha = registro["senha"].ToString();
            c.Nasc = Convert.ToDateTime(registro["nasc"]);
            c.Sexo = Convert.ToChar(registro["sexo"].ToString());

            if (registro["imagem"] != DBNull.Value)
            {
                c.ImageBase64 = Convert.ToBase64String(registro["imagem"] as byte[]);
            }

            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "clientes";
        }
        /*
        public bool ClienteViewModel Login(string email, string senha)
        {            
            var p = new SqlParameter[]
            {
                new SqlParameter("email", email),
                new SqlParameter("senha", senha),
                new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spLogin", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return true;
        }*/
    }
}
