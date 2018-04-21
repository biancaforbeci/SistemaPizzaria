using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PedidoBebidas
    {
        public int PedidoBebidasID { get; set; }
        public int NumeroPedidoID { get; set; }
        public int ClientesBebidasID { get; set; }
        public Pedido _Pedido { get; set; }
        public ClientesBebidas _ClientesBebidas { get; set; }
    }
}
