using Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class ClienteController
    {
        static private List<Cliente> ClientesCadastrados = new List<Cliente>();
        int UltimoID = 0;

        public void SalvarCliente(Cliente e)
        {
            int id = UltimoID + 1;
            UltimoID = id;
            e.PessoaID = id;
            ClientesCadastrados.Add(e);
        }

        public Cliente PesquisarPorIDCliente(int id)
        {
            var a = from x in ClientesCadastrados
                    where x.PessoaID.Equals(id)
                    select x;

            if (a != null)
            {
                return a.FirstOrDefault();
            }
            return null;
        }

        public List<Cliente> MostrarLista()
        {
            return ClientesCadastrados;
        }

        public void EditarCliente(int id,Cliente CliAtual)
        {
            Cliente CliAntigo = PesquisarPorIDCliente(id);

            CliAntigo.Nome = CliAtual.Nome;
            CliAntigo.Telefone = CliAtual.Telefone;
            CliAntigo.Cpf = CliAtual.Cpf;

        }

        public void ExcluirCLiente(int id)
        {
            Cliente cliExcluir = PesquisarPorIDCliente(id);

            if (cliExcluir != null)
            {
                ClientesCadastrados.Remove(cliExcluir);
            }
           
        }

    }
}
