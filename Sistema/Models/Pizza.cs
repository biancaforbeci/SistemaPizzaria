using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Pizza
    {
        public int PizzaID { get; set; }
        public string Nome { get; set; }
        public string Ingredientes { get; set; }
        public double Preco { get; set; }
    }
}
