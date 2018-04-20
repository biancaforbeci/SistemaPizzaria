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
    }
}
