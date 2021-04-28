using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trab_LP_BD_v1.Models;
using Trab_LP_BD_v1.DAO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Trab_LP_BD_v1.Controllers
{
    public class FuncionarioController : PadraoController<FuncionarioViewModel>
    {
        public FuncionarioController()
        {
            GeraProximoId = true;
            DAO = new FuncionarioDAO();
        }
        protected override void PreencheDadosParaView(string Operacao, FuncionarioViewModel model)
        {            
            base.PreencheDadosParaView(Operacao, model);
        }        

        protected override void ValidaDados(FuncionarioViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (string.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");            
        }
    }
}