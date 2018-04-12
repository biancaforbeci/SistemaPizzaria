using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Pedido
    {
        public int PedidoID { get; set; }
        public int Quantidade { get; set; }
        public int Produto_ID { get; set; }   //lista de pedidos de pizza
        public double ValorTotal { get; set; }

    }
}
