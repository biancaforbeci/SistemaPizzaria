using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class AdministradorController
    {
        public bool Permissao(string login, string senha)
        {
            if((login.Equals("adm") && senha.Equals("123")))
            {
                return true;
            }
            return false;
        }
    }
}
