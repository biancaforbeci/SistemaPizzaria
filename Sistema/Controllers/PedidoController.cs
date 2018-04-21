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

        public static void MudarStatus(int  pedidoAntigoID, string status)
        {
            Pedido pedidoEdit = PesquisarPorID(pedidoAntigoID);

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

        public static Pedido PesquisarPorID(int IDPedido)
        {
            return ContextoSingleton.Instancia.TblPedido.Find(IDPedido);
        }
    }
}
