using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ClientesPizzas
    {
        public int ClientesPizzasID { get; set; }
        public int ClienteID { get; set; }        
        public int PizzaID { get; set; }
        public DateTime Data { get; set; }
        public double Preco { get; set; }
        public virtual Cliente _Cliente { get; set; }
        public virtual Pizza _Pizza { get; set; }
        
    }
}
