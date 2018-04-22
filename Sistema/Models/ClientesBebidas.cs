using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ClientesBebidas
    {
        public int ClientesBebidasID { get; set; }
        public int BebidaID { get; set; }
        public int ClienteID { get; set; }
        public int NumReferencia { get; set; }
        public virtual Bebida _Bebida { get; set; }
        public virtual Cliente _Cliente { get; set; }
    }
}
