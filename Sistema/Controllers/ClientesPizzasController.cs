using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
   public class ClientesPizzasController
    {
        public static void SalvarItem(ClientesPizzas novo)
        {
            ContextoSingleton.Instancia.TblClientesPizzas.Add(novo);
            ContextoSingleton.Instancia.SaveChanges();
        }

        public static List<ClientesPizzas> PesquisarPorClientePedidos(int id)
        {
            var c = (from x in ContextoSingleton.Instancia.TblClientesPizzas
                     where x.ClienteID.Equals(id)
                     select x).ToList();

            if (c.Count > 0)
            {
                return c;
            }
            else
            {
                return null;
            }
        }
    }
}
