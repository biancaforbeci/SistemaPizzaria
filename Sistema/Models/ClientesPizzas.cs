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
        public double Preco { get; set; }
        public string Tamanho_Pizza { get; set; }
        public int NumReferencia { get; set; } //Se o cliente pedir novamente outra hora, terá outro número desse para buscar junto com ID no Pesquisa Clientes Pedidos
        public virtual Cliente _Cliente { get; set; }
        public virtual Pizza _Pizza { get; set; }
        
    }
}
