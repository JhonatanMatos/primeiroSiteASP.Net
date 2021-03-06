using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trab_LP_BD_v1.Models;
using Trab_LP_BD_v1.DAO;

namespace Trab_LP_BD_v1.Controllers
{
    public class CategoriaController : PadraoController<CategoriaViewModel>
    {
        public CategoriaController()
        {
            GeraProximoId = true;
            DAO = new CategoriaDAO();
        }

        protected override void PreencheDadosParaView(string Operacao, CategoriaViewModel model)
        {
            /*if (Operacao == "I")
                model.DataNascimento = DateTime.Now;*/
            //PreparaListaLocalParaCombo();
            base.PreencheDadosParaView(Operacao, model);
        }
        #region ListaCombo
        /*
        private void PreparaListaLocalParaCombo()
        {
            LocalDAO localDao = new LocalDAO();
            var locais = localDao.Listagem();
            List<SelectListItem> listaLocais = new List<SelectListItem>();
            listaLocais.Add(new SelectListItem("Selecione um local...", "0"));
            foreach (var local in locais)
            {
                SelectListItem item = new SelectListItem(local.Rua, local.Id.ToString());
                listaLocais.Add(item);
            }
            ViewBag.Locais = listaLocais;
        }
        */
        #endregion
        protected override void ValidaDados(CategoriaViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (string.IsNullOrEmpty(model.Descricao))
                ModelState.AddModelError("Descricao", "Preencha a descrição.");
        }

    }
}