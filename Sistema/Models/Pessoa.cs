using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Pessoa
    {
        public int PessoaID { get; set; }

        public String Nome { get; set; }

        public String Cpf { get; set; }

        public String Telefone { get; set; }

        public int EnderecoID { get; set; }

        public Endereco _Endereco { get; set; }

        public Pedido _Pedido { get; set; }

    }
}
