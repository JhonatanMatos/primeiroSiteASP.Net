using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Trab_LP_BD_v1.Models;

namespace Trab_LP_BD_v1.DAO
{
    public class ProdutosDAO : PadraoDAO<ProdutosViewModel>
    {
        protected override SqlParameter[] CriaParametros(ProdutosViewModel model)
        {
            object imgByte = model.ImageByte();
            if (imgByte == null)
                imgByte = DBNull.Value;

            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("descricao", model.Descricao);
            parametros[2] = new SqlParameter("categoriaId", model.CategoriaId);
            parametros[3] = new SqlParameter("preco", model.Preco);
            parametros[4] = new SqlParameter("imagem", imgByte);

            return parametros;
        }

        protected override ProdutosViewModel MontaModel(DataRow registro)
        {
            ProdutosViewModel p = new ProdutosViewModel();
            p.Id = Convert.ToInt32(registro["id"]);
            p.Descricao = registro["descricao"].ToString();
            p.CategoriaId = Convert.ToInt32(registro["categoriaId"]);
            p.Preco = Convert.ToDouble(registro["preco"]);

            if (registro["imagem"] != DBNull.Value)
            {
                p.ImageBase64 = Convert.ToBase64String(registro["imagem"] as byte[]);
            }
            return p;
        }

        protected override void SetTabela()
        {
            Tabela = "produtos";
        }
    }
}
