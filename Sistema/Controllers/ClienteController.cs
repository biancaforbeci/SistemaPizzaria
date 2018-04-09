using Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controllers
{
    public class ClienteController
    {
        static private List<Cliente> ClientesCadastrados = new List<Cliente>();

        SqlConnection conn = new SqlConnection(@"Data Source=SAMSUNG\MSSQL;Initial Catalog=systempizza;Integrated Security=True");
               
        public void SalvarCliente(string txtNome, string txtCPF, string txtTelefone)
        {
            
            string sql = "INSERT INTO Clientes(Nome, CPF, Telefone) VALUES (@Nome, @CPF, @Telefone)";
            try
            {

                SqlCommand comando = new SqlCommand(sql, conn);

                
                comando.Parameters.Add(new SqlParameter("@Nome",txtNome));
                comando.Parameters.Add(new SqlParameter("@CPF",txtCPF));
                comando.Parameters.Add(new SqlParameter("@Telefone", txtTelefone));

                conn.Open();

                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro concluído");
                
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            

        }

        public DataTable ExibirDados()
        {
            try
            {
                string sql = "SELECT ID,Nome, CPF, Telefone, Rua, Numero, Bairro, Complemento, Referencia FROM Clientes, Enderecos WHERE ID_End = ID_Endereco";
                conn.Open();
                SqlCommand comando = new SqlCommand(sql, conn);

                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.SelectCommand = comando;

                DataTable Dt = new DataTable("Clientes");
                Da.Fill(Dt);

                conn.Close();
                return Dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public DataTable ProcurarTelefone(string telefone_pedido)
        {
            try
            {
                string sql = "SELECT ID,Nome, CPF, Telefone FROM Clientes WHERE Telefone=@Telefone";
                conn.Open();
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.Parameters.Add(new SqlParameter("@Telefone", telefone_pedido));
                comando.ExecuteNonQuery();

                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.SelectCommand = comando;

                DataTable Dt = new DataTable("Clientes");
                Da.Fill(Dt);

                conn.Close();
                return Dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }


    }

    


    /*      public Cliente PesquisarPorIDCliente(int id)
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
      */
}
