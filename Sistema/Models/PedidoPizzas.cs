using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PedidoPizzas
    {
        public int PedidoPizzasID { get; set; }
        public int PedidoID { get; set; }
        public int ClientesPizzasID { get; set; }
        public Pedido _Pedido { get; set; }
        public ClientesPizzas _ClientesPizzas { get; set;}
    }
}
