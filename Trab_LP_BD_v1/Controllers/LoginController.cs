using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trab_LP_BD_v1.DAO;
using Trab_LP_BD_v1.Models;

namespace Trab_LP_BD_v1.Controllers
{
    public class LoginController : Controller
    {
        ClienteDAO c = new ClienteDAO();
        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult Salvar(LoginViewModel l)
        {
            bool existeUser = false;
            LoginDAO dao = new LoginDAO();
            try
            {
                var lista = dao.Consulta_Login(l.Usuario);

                if (lista != null)
                    if (lista.Usuario == l.Usuario)
                        existeUser = true;

                if (existeUser == false)
                    dao.Insert(l);
                else
                {
                    throw new Exception("Usuário já existente!");
                }
                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                ViewBag.Erro = "Ocorreu um erro: " + erro.Message;
                return View("Form");
            }
        }

        public IActionResult FazLogin(string email, string senha)
        {
            LoginDAO log = new LoginDAO();
            var lista = log.Consulta_Login(email);
            bool existeUser = false;

            if (lista != null)
                if (lista.Usuario == email && lista.Senha == senha)
                    existeUser = true;
                    
            //Este é apenas um exemplo, aqui você deve consultar na sua tabela de usuários
            //se existem esse usuário e senha
            if (existeUser)
            {
                HttpContext.Session.SetString("Logado", "true");
                ViewBag.Logado = HelperControllers.VerificaUserLogado(HttpContext.Session);
                return RedirectToAction("index", "Home");
            }
            else if (email == "admin" && senha == "1234")
            {
                HttpContext.Session.SetString("Admin", "true");
                ViewBag.Admin = HelperControllers.VerificaUserAdmin(HttpContext.Session);
                return RedirectToAction("index", "Home");
            }
            else
            {
                ViewBag.Erro = "Usuário ou senha inválidos!";
                return View("Index");
            }
        }        

        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}