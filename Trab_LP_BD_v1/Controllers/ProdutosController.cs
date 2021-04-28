using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trab_LP_BD_v1.Models;
using Trab_LP_BD_v1.DAO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Trab_LP_BD_v1.Controllers
{
    public class ProdutosController : PadraoController<ProdutosViewModel>
    {
        public ProdutosController()
        {
            GeraProximoId = true;
            DAO = new ProdutosDAO();
        }

        public byte[] ConvertImageToByte(IFormFile file)
        {
            if (file != null)
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            else
                return null;
        }

        protected override void PreencheDadosParaView(string Operacao, ProdutosViewModel model)
        {            
            base.PreencheDadosParaView(Operacao, model);
        }
        protected override void ValidaDados(ProdutosViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (string.IsNullOrEmpty(model.Descricao))
                ModelState.AddModelError("Descricao", "Preencha a descrição.");
            if (model.CategoriaId < 0)
                ModelState.AddModelError("CategoriaId", "Campo obrigatório.");
            if (model.Preco < 0)
                ModelState.AddModelError("Preco", "Informe o preço.");

            if (model.Imagem == null && operacao == "I")
                ModelState.AddModelError("Imagem", "Escolha uma imagem.");
            if (model.Imagem != null && model.Imagem.Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("Imagem", "Imagem limitada a 2 mb.");


            if (ModelState.IsValid)
            {
                //na alteração, se não foi informada a imagem, iremos manter a que já estava salva.
                if (operacao == "A" && model.Imagem == null)
                {
                    ProdutosViewModel produto = DAO.Consulta(Convert.ToUInt16(model.Id));
                    model.ImagemEmByte = produto.ImagemEmByte;
                }
                else
                {
                    model.ImagemEmByte = ConvertImageToByte(model.Imagem);
                }
            }
        }

        private void PreparaListaCategoriasParaCombo()
        {
            CategoriaDAO categoriaDao = new CategoriaDAO();
            var categorias = categoriaDao.ListaCategorias();
            List<SelectListItem> listaCategorias = new List<SelectListItem>();
            listaCategorias.Add(new SelectListItem("Selecione uma categoria...", "0"));
            foreach (var cidade in categorias)
            {
                SelectListItem item = new SelectListItem(cidade.Descricao, cidade.Id.ToString());
                listaCategorias.Add(item);
            }
            ViewBag.Categorias = listaCategorias;
        }
    }
}