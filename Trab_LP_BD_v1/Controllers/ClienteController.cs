using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trab_LP_BD_v1.Models;
using Trab_LP_BD_v1.DAO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Trab_LP_BD_v1.Controllers
{
    public class ClienteController : PadraoController<ClienteViewModel>
    {
        public ClienteController()
        {
            GeraProximoId = true;
            DAO = new ClienteDAO();
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

        protected override void PreencheDadosParaView(string Operacao, ClienteViewModel model)
        {            
            base.PreencheDadosParaView(Operacao, model);
        }        
        protected override void ValidaDados(ClienteViewModel model, string operacao)
        {            

            base.ValidaDados(model, operacao);
            if (string.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");
            if (string.IsNullOrEmpty(model.CEP))
                ModelState.AddModelError("Cep", "Campo obrigatório.");
            if (string.IsNullOrEmpty(model.Endereco))
                ModelState.AddModelError("Endereco", "Campo obrigatório.");
            if (string.IsNullOrEmpty(model.Bairro))
                ModelState.AddModelError("Bairro", "Campo obrigatório.");
            if (string.IsNullOrEmpty(model.Cidade))
                ModelState.AddModelError("Cidade", "Campo obrigatório.");
            if (string.IsNullOrEmpty(model.Estado))
                ModelState.AddModelError("Estado", "Campo obrigatório.");
            if (string.IsNullOrEmpty(model.Telefone))
                ModelState.AddModelError("Telefone", "Informe o telefone.");
            if (string.IsNullOrEmpty(model.Email))
                ModelState.AddModelError("Email", "Informe o email.");
            if (string.IsNullOrEmpty(model.Senha))
                ModelState.AddModelError("Senha", "Informe a senha.");
            if (model.Nasc >= DateTime.Now)
                ModelState.AddModelError("Nasc", "Data Inválida.");
            if (string.IsNullOrEmpty(model.Sexo.ToString()))
                ModelState.AddModelError("Sexo", "Informe o sexo.");

            if (model.Imagem == null && operacao == "I")
                ModelState.AddModelError("Imagem", "Escolha uma imagem.");
            if (model.Imagem != null && model.Imagem.Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("Imagem", "Imagem limitada a 2 mb.");
            if (ModelState.IsValid)
            {
                //na alteração, se não foi informada a imagem, iremos manter a que já estava salva.
                if (operacao == "A" && model.Imagem == null)
                {
                    ClienteViewModel cliente = DAO.Consulta(Convert.ToUInt16(model.Id));
                    model.ImagemEmByte = cliente.ImagemEmByte;
                }
                else
                {
                    model.ImagemEmByte = ConvertImageToByte(model.Imagem);
                }
            }
        }
    }
}