using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class PedidoController
    {
        // INSERT
        public static void SalvarPedido(Pedido novo)
        {
            ContextoSingleton.Instancia.TblPedido.Add(novo);
            ContextoSingleton.Instancia.SaveChanges();
        }

        public static List<Pedido> ListarTodosPedidos()
        {

            return ContextoSingleton.Instancia.TblPedido.ToList(); //IQueryable
        }

        public static bool PesquisaNumPedido(int num)
        {
            var c = (from x in ContextoSingleton.Instancia.TblPedido
                     where x.NumPedido.Equals(num)
                     select x).ToList();

            if (c.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Pedido> ProcuraPedidoFinalizados()
        {
            var c = (from x in ContextoSingleton.Instancia.TblPedido
                     where x.Status.Contains("Finalizado")
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

        public static List<Pedido> ProcuraPedidoPendentes()
        {
            var c = (from x in ContextoSingleton.Instancia.TblPedido
                     where x.Status.Contains("Em andamento")
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

        public static Pedido PesquisarPorID(int IDPedido)
        {
            return ContextoSingleton.Instancia.TblPedido.Find(IDPedido);
        }
    }
}
