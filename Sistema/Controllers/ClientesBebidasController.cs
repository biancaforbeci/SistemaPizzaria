using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class ClientesBebidasController
    {
        public static void SalvarItem(ClientesBebidas novo)
        {
            ContextoSingleton.Instancia.TblClientesBebidas.Add(novo);
            ContextoSingleton.Instancia.SaveChanges();
        }

        public static List<ClientesBebidas> PesquisarClientePedidos(int cliID)
        {
            var c = (from x in ContextoSingleton.Instancia.TblClientesBebidas.Include("_Cliente")
                     where x.ClienteID.Equals(cliID)
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

        public static void ExcluirSelecao(int id)
        {
            ClientesBebidas bebidaExcluir = ContextoSingleton.Instancia.TblClientesBebidas.Find(id);

            ContextoSingleton.Instancia.Entry(bebidaExcluir).State =
                System.Data.Entity.EntityState.Deleted;
            ContextoSingleton.Instancia.SaveChanges();
        }

        public static void ExcluirPedidosCliente(int cliID)
        {
            var c = (from x in ContextoSingleton.Instancia.TblClientesBebidas.Include("_Cliente")
                     where x.ClienteID.Equals(cliID)
                     select x).ToList();
            foreach (var item in c)
            {
                ContextoSingleton.Instancia.Entry(item).State =
               System.Data.Entity.EntityState.Deleted;
                ContextoSingleton.Instancia.SaveChanges();
            }
        }
    }
}
