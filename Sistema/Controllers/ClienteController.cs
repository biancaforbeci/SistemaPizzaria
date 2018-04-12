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

        public static List<Cliente> ListarTodosClientes()
        {
            MeuContexto bancoDados = new MeuContexto();
            return bancoDados.TblClientes.ToList(); //IQueryable
        }

        public static Cliente CarregarPorID(int id)
        {
            MeuContexto bancoDados = new MeuContexto();
            return bancoDados.TblClientes.Find(id);
        }

        public static void EditarCliente(int id, Cliente novoCliente)
        {
            MeuContexto bancoDados = new MeuContexto();
            Cliente clienteEdit = PesquisarPorID(id);

            if(clienteEdit != null)
            {
                clienteEdit.Nome = novoCliente.Nome;
                clienteEdit.Cpf = novoCliente.Cpf;
                clienteEdit.Telefone = novoCliente.Telefone;
            }

            bancoDados.Entry(clienteEdit).State =
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

        public static Cliente PesquisarPorNome(string nome)
        {
            MeuContexto cont = new MeuContexto();

            var c = from x in cont.TblClientes
                    where x.Nome.ToLower().Contains(nome)
                    select x;

            if (c != null)
            {
                return c.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public static Cliente PesquisarPorID(int IDCliente)
        {
            MeuContexto cont = new MeuContexto();
            return cont.TblClientes.Find(IDCliente);
        }


    }
}