using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Trab_LP_BD_v1.Controllers

{
    public class HelperControllers
    {
        public static Boolean VerificaUserLogado(ISession session)
        {
            string logado = session.GetString("Logado");
            if (logado == null)
            {
                VerificaUserAdmin(session);

                return false;
            }
            else
                return true;
        }

        public static Boolean VerificaUserAdmin(ISession session)
        {
            string admin = session.GetString("Admin");
            if (admin == null)
                return false;
            else
            {                
                return true;
            }
        }
    }
}