using Models;
using Sistema.Models.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Controllers
{
    public class ClienteController
    {
        // INSERT
        public static void SalvarCliente(Cliente cliente)
        {
            MeuContexto bancoDados = new MeuContexto();

            bancoDados.TblClientes.Add(cliente);
            bancoDados.SaveChanges();
        }

        // SELECT *
        public static List<Cliente> ListarTodosClientes()
        {
            MeuContexto bancoDados = new MeuContexto();
            return bancoDados.TblClientes.ToList();
        }


        // SELECT BY ID
        public static Cliente CarregarPorID(int id)
        {
            MeuContexto bancoDados = new MeuContexto();
            return bancoDados.TblClientes.Find(id);
        }

        public static void EditarCliente(int id, Cliente novoCliente)
        {
            MeuContexto bancoDados = new MeuContexto();
            Cliente clienteAtual = bancoDados.TblClientes.Find(id);

            clienteAtual.Nome = novoCliente.Nome;
            clienteAtual.Cpf = novoCliente.Cpf ;
            clienteAtual.Telefone = novoCliente.Telefone;
            
            bancoDados.Entry(clienteAtual).State =
                System.Data.Entity.EntityState.Modified;

            bancoDados.SaveChanges();
        }

        public static void ExcluirCliente(int id)
        {
            MeuContexto bancoDados = new MeuContexto();
            Cliente clienteAtual = bancoDados.TblClientes.Find(id);

            bancoDados.Entry(clienteAtual).State =
                System.Data.Entity.EntityState.Deleted;

            bancoDados.SaveChanges();

        }
    }
}