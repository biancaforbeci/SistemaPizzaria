using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public abstract class Pessoa
    {
        public int PessoaID { get; set; }

        public String Nome { get; set; }

        public String Cpf { get; set; }

        public String Telefone { get; set; }

        public int EnderecoID { get; set; }


    }
}
