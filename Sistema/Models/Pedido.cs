using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Pedido
    {
        public int ProdutoID { get; set;}
        public ClientesPizzas _ClientesPizzas { get; set; }
        public int ClientesPizzasID { get; set; }
        public int NumPedido { get; set; }
        public double ValorTotal { get; set;}        
        public string Status { get; set; }
        public string Tamanho_Pizza { get; set; }
    }
}
