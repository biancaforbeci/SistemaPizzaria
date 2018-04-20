using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Pedido
    {
        [Key]
        public int NumeroPedidoID { get; set;}
        public double ValorTotal { get; set;}        
        public string Status { get; set; }
        public DateTime Data { get; set; }
        
    }
}
