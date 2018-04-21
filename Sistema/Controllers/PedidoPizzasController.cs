using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class PedidoPizzasController
    {
        public static void SalvarPedido(PedidoPizzas novo)
        {
            ContextoSingleton.Instancia.TblPedidoPizzas.Add(novo);
            ContextoSingleton.Instancia.SaveChanges();
        }

        public static List<PedidoPizzas> ProcuraPedidoSaiuParaEntrega()
        {
            var c = (from x in ContextoSingleton.Instancia.TblPedidoPizzas.Include("_ClientesPizzas").Include("_Pedido")
                     where x._Pedido.Status.Contains("SAIU PARA ENTREGA") && x._Pedido.NumeroPedidoID.Equals(x.NumeroPedidoID) && x.ClientesPizzasID.Equals(x._ClientesPizzas.ClientesPizzasID)
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

        public static List<PedidoPizzas> ProcuraPedidoPendentes()
        {
            var c = (from x in ContextoSingleton.Instancia.TblPedidoPizzas.Include("_ClientesPizzas").Include("_Pedido")
                     where x._Pedido.Status.Contains("EM PRODUÇÃO") && x.NumeroPedidoID.Equals(x._Pedido.NumeroPedidoID) &&  x.ClientesPizzasID.Equals(x._ClientesPizzas.ClientesPizzasID)
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

        public static List<PedidoPizzas> ProcuraPedidoFinalizado()
        {
            var c = (from x in ContextoSingleton.Instancia.TblPedidoPizzas.Include("_ClientesPizzas").Include("_Pedido")
                     where x._Pedido.Status.Contains("FINALIZADO") && x._Pedido.NumeroPedidoID.Equals(x.NumeroPedidoID) && x.ClientesPizzasID.Equals(x._ClientesPizzas.ClientesPizzasID) 
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
