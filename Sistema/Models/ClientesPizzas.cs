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
        public DateTime data { get; set; }
    }
}
