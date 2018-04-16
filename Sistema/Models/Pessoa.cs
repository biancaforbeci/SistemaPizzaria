using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Pessoa
    {
        public String Nome { get; set; }

        public String Cpf { get; set; }

        public String Telefone { get; set; }

        public int EnderecoID { get; set; }
        
        public virtual Endereco _Endereco { get; set; }        

    }
}
