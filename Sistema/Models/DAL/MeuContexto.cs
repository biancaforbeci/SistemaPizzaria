using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Models.DAL
{
    public class MeuContexto : DbContext     //Espelho do banco de dados o contexto - mostra estrutura do banco
    {
        public MeuContexto() : base("strConn")
        {

        }

        public DbSet<Cliente> TblClientes { get; set; }

        public DbSet<Endereco>TblEnderecos { get; set; }

    }
}
