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
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Cadastro concluído");
            }
            

        }

        public DataTable ExibirDados()
        {
            string sql = "SELECT * FROM Clientes";
            try
            {                
                SqlCommand comando = new SqlCommand(sql, conn);
                SqlDataAdapter Da = new SqlDataAdapter();
                Da.SelectCommand = comando;

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(Da);

                DataTable Dt = new DataTable();
                Dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                Da.Fill(Dt);

                
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
