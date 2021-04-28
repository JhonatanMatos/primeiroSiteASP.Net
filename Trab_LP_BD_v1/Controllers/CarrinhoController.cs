using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Trab_LP_BD_v1.DAO;
using Trab_LP_BD_v1.Models;

namespace Trab_LP_BD_v1.Controllers
{
    public class CarrinhoController : Controller
    {

        public IActionResult Index()
        {
            ProdutosDAO dao = new ProdutosDAO();
            var listaDeProdutos = dao.Listagem();

            var carrinho = ObtemCarrinhoNaSession();

            @ViewBag.TotalCarrinho = carrinho.Sum(c => c.Quantidade);

            return View(listaDeProdutos);
        }


        public IActionResult Detalhes(int idProduto)
        {
            List<CarrinhoViewModel> carrinho = ObtemCarrinhoNaSession();

            ProdutosDAO dao = new ProdutosDAO();
            var modelProduto = dao.Consulta(idProduto);

            CarrinhoViewModel carrinhoModel = carrinho.Find(c => c.ProdutoId == idProduto);
            if (carrinhoModel == null)
            {
                carrinhoModel = new CarrinhoViewModel();
                carrinhoModel.ProdutoId = idProduto;
                carrinhoModel.Nome = modelProduto.Descricao;
                carrinhoModel.Quantidade = 0;
            }

            // preenche a imagem
            carrinhoModel.ImagemEmBase64 = modelProduto.ImageBase64;
            return View(carrinhoModel);
        }

        private List<CarrinhoViewModel> ObtemCarrinhoNaSession()
        {
            List<CarrinhoViewModel> carrinho = new List<CarrinhoViewModel>();
            string carrinhoJson = HttpContext.Session.GetString("carrinho");
            if (carrinhoJson != null)
                carrinho = JsonConvert.DeserializeObject<List<CarrinhoViewModel>>(carrinhoJson);

            return carrinho;
        }

        public IActionResult AdicionarCarrinho(int ProdutoId, int Quantidade)
        {
            List<CarrinhoViewModel> carrinho = ObtemCarrinhoNaSession();

            CarrinhoViewModel carrinhoModel = carrinho.Find(c => c.ProdutoId == ProdutoId);

            if (carrinhoModel != null && Quantidade == 0)
            {
                //tira do carrinho
                carrinho.Remove(carrinhoModel);
            }
            else if (carrinhoModel == null && Quantidade > 0)
            {
                //não havia no carrinho, vamos adicionar
                ProdutosDAO dao = new ProdutosDAO();
                var modelProduto = dao.Consulta(ProdutoId);

                carrinhoModel = new CarrinhoViewModel();
                carrinhoModel.ProdutoId = ProdutoId;
                carrinhoModel.Nome = modelProduto.Descricao;
                carrinhoModel.Valor = modelProduto.Preco * Quantidade;
                carrinho.Add(carrinhoModel);
            }

            if (carrinhoModel != null)
                carrinhoModel.Quantidade = Quantidade;

            string carrinhoJson = JsonConvert.SerializeObject(carrinho);
            HttpContext.Session.SetString("carrinho", carrinhoJson);

            return RedirectToAction("Index");
        }


        public IActionResult Visualizar()
        {
            var carrinho = ObtemCarrinhoNaSession();
            return View(carrinho);
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                context.Result = RedirectToAction("Index", "Login");
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }
        }
        public IActionResult EfetuarPedido()
        {
            using (var transacao = new System.Transactions.TransactionScope())
            {
                PedidoViewModel pedido = new PedidoViewModel();
                pedido.Data = DateTime.Now;
                PedidoDAO pedidoDAO = new PedidoDAO();
                int idPedido = pedidoDAO.Insert(pedido);
                PedidoItemDAO itemDAO = new PedidoItemDAO();
                var carrinho = ObtemCarrinhoNaSession();
                foreach (var elemento in carrinho)
                {
                    PedidoItemViewModel item = new PedidoItemViewModel();
                    item.PedidoId = idPedido;
                    item.ProdutoId = elemento.ProdutoId;
                    item.Qtde = elemento.Quantidade;
                    item.Valor = elemento.Valor;
                    itemDAO.Insert(item);
                }
                transacao.Complete();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}