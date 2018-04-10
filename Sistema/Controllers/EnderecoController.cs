using Models;
using Sistema.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controllers
{
    public class EnderecoController
    {

        public static void SalvarEndereco(Endereco endereco)
        {
            MeuContexto bancoDados = new MeuContexto();

            bancoDados.TblEnderecos.Add(endereco);
            bancoDados.SaveChanges();
        }

        // SELECT *
        public static List<Endereco> ListarTodosClientes()
        {
            MeuContexto bancoDados = new MeuContexto();
            return bancoDados.TblEnderecos.ToList();
        }


        // SELECT BY ID
        public static Endereco CarregarPorID(int id)
        {
            MeuContexto bancoDados = new MeuContexto();
            return bancoDados.TblEnderecos.Find(id);
        }

        public static void EditarEnderecp(int id, Endereco NovoEnd)
        {
            MeuContexto bancoDados = new MeuContexto();
            Endereco EndAtual = bancoDados.TblEnderecos.Find(id);

            EndAtual.Rua = NovoEnd.Rua;
            EndAtual.Numero = NovoEnd.Numero;
            EndAtual.Complemento = NovoEnd.Complemento;
            EndAtual.Bairro = NovoEnd.Bairro;
            EndAtual.Referencia = NovoEnd.Referencia;
           
            bancoDados.Entry(EndAtual).State =
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