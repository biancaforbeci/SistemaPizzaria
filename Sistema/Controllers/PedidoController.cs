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

        public static void MudarStatus(Pedido pedidoAntigo, string status)
        {
            Pedido pedidoEdit = PesquisarPorID(pedidoAntigo.NumeroPedidoID);

           if (pedidoEdit != null)
            {
              pedidoEdit.Status = status;
           }

           ContextoSingleton.Instancia.Entry(pedidoEdit).State =
            System.Data.Entity.EntityState.Modified;
            ContextoSingleton.Instancia.SaveChanges();
        }

        public static List<Pedido> ListarTodosPedidos()
        {

            return ContextoSingleton.Instancia.TblPedido.Include("_ClientesPizzas").Include("_ClientesBebidas").ToList();
        }        

        public static List<Pedido> ProcuraPedidoSaiuParaEntrega()
        {
            var c = (from x in ContextoSingleton.Instancia.TblPedido.Include("_ClientesPizzas").Include("_ClientesBebidas")
                     where x.Status.Contains("SAIU PARA ENTREGA")
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
            var c = (from x in ContextoSingleton.Instancia.TblPedido.Include("_ClientesPizzas").Include("_ClientesBebidas")
                     where x.Status.Contains("EM PRODUÇÃO")
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

        public static List<Pedido> ProcuraPedidoFinalizado()
        {
            var c = (from x in ContextoSingleton.Instancia.TblPedido.Include("_ClientesPizzas").Include("_ClientesBebidas")
                     where x.Status.Contains("FINALIZADO")
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
