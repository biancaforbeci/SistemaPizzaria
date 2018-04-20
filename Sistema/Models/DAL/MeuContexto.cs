using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Models.DAL
{
    public class MeuContexto : DbContext
    {
        public MeuContexto() : base("strConn")
        {
            Database.SetInitializer( new DropCreateDatabaseIfModelChanges<MeuContexto>());
        }

        public DbSet<Cliente> TblClientes { get; set; }

        public DbSet<Endereco>TblEnderecos { get; set; }

        public DbSet<Pizza> TblPizza { get; set; }

        public DbSet<Pedido> TblPedido {get;set;}

        public DbSet<Bebida> TblBebida { get; set; }

        public DbSet<ClientesPizzas> TblClientesPizzas { get; set; }

        public DbSet<ClientesBebidas> TblClientesBebidas { get; set; }

    }
}
