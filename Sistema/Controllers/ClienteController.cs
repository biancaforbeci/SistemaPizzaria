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
            ContextoSingleton.Instancia.TblClientes.Add(cliente);
            ContextoSingleton.Instancia.SaveChanges();
        }

        public static List<Cliente> ListarTodosClientes()
        {
           
            return ContextoSingleton.Instancia.TblClientes.Include("_Endereco").ToList(); //IQueryable
        }

        public static void EditarCliente(int id, Cliente novoCliente)
        {
            
            Cliente clienteEdit = PesquisarPorID(id);

            if(clienteEdit != null)
            {
                clienteEdit.Nome = novoCliente.Nome;
                clienteEdit.Cpf = novoCliente.Cpf;
                clienteEdit.Telefone = novoCliente.Telefone;
            }

            ContextoSingleton.Instancia.Entry(clienteEdit).State =
                System.Data.Entity.EntityState.Modified;

            ContextoSingleton.Instancia.SaveChanges(); 
        }

        public static void ExcluirCliente(int id)
        {
            
            Cliente clienteAtual = ContextoSingleton.Instancia.TblClientes.Find(id);

            ContextoSingleton.Instancia.Entry(clienteAtual).State =
                System.Data.Entity.EntityState.Deleted;
            ContextoSingleton.Instancia.SaveChanges();

        }

        public static List<Cliente> PesquisarPorNome(string nome)
        {

            var c = (from x in ContextoSingleton.Instancia.TblClientes
                     where x.Nome.ToLower().Equals(nome.ToLower())
                     select x).ToList();

            if (c.Count > 0)
            {
                return c;
            }
            else
            {
                return null;
            }
        }

        public static Cliente PesquisarPorID(int IDCliente)
        {
            return ContextoSingleton.Instancia.TblClientes.Include("_Endereco").SingleOrDefault(x => x.ClienteID == IDCliente && x._Endereco.EnderecoID==x.EnderecoID);
        }

        public static List<Cliente> PesquisarPorTelefone(string tel)
        {            
            var c = (from x in ContextoSingleton.Instancia.TblClientes.Include("_Endereco")
                     where x.Telefone.Contains(tel) && x._Endereco.EnderecoID == x.EnderecoID
                     select x).ToList();

            if (c.Count > 0)
            {
                return c;
            }
            else
            {
                return null;
            }
        }
        
    }
}