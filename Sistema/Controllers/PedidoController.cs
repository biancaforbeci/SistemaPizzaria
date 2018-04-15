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

        public static void EditarPedido(int id, Pedido novoPedido)
        {

            Pedido pedidoEdit = PesquisarPorID(id);

            if (pedidoEdit != null)
            {
                pedidoEdit.Produto_ID = novoPedido.Produto_ID;
                pedidoEdit.Quantidade = novoPedido.Quantidade;
                pedidoEdit.ValorTotal = novoPedido.ValorTotal;
            }

            ContextoSingleton.Instancia.Entry(pedidoEdit).State =
                System.Data.Entity.EntityState.Modified;

            ContextoSingleton.Instancia.SaveChanges();
        }

        public static void ExcluirPedido(int id)
        {

            Pedido pedidoAtual = ContextoSingleton.Instancia.TblPedido.Find(id);

            ContextoSingleton.Instancia.Entry(pedidoAtual).State =
                System.Data.Entity.EntityState.Deleted;
            ContextoSingleton.Instancia.SaveChanges();

        }        

        public static Pedido PesquisarPorID(int IDPedido)
        {
            return ContextoSingleton.Instancia.TblPedido.Find(IDPedido);
        }
    }
}
