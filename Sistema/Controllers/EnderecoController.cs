using Models;
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

        SqlConnection conn = new SqlConnection(@"Data Source=SAMSUNG\MSSQL;Initial Catalog=systempizza;Integrated Security=True");

        public void SalvarEndereco(string txtRua, string txtNumero, string txtBairro, string txtComplemento, string txtReferencia)
        {

            string sql = "INSERT INTO Enderecos(Rua, Numero, Bairro, Complemento, Referencia) VALUES (@Rua, @Numero, @Bairro,@Complemento,@Referencia)";
            try
            {

                SqlCommand comando = new SqlCommand(sql, conn);

                comando.Parameters.Add(new SqlParameter("@Rua", txtRua));
                comando.Parameters.Add(new SqlParameter("@Numero", txtNumero));
                comando.Parameters.Add(new SqlParameter("@Bairro", txtBairro));
                comando.Parameters.Add(new SqlParameter("@Complemento", txtComplemento));
                comando.Parameters.Add(new SqlParameter("@Referencia", txtReferencia));

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
            }

        }

        /*      public Endereco PesquisarPorIDEndereco(int id)
              {
                  var a = from x in EnderecosCadastrados
                          where x.EnderecoID.Equals(id)
                          select x;

                  if (a != null)
                  {
                      return a.FirstOrDefault();
                  }
                  return null;
              }

              public void EditarEndereco(int id, Endereco EndAtual)
              {
                  Endereco EndAntigo = PesquisarPorIDEndereco(id);

                  EndAntigo.Rua = EndAtual.Rua;
                  EndAntigo.Numero = EndAtual.Numero;
                  EndAntigo.Bairro = EndAtual.Bairro;
                  EndAntigo.Complemento = EndAtual.Complemento;
                  EndAntigo.Referencia = EndAtual.Referencia;
              }

              public void ExcluirEndereco(int id)
              {
                  Endereco EndExcluir = PesquisarPorIDEndereco(id);

                  if (EndExcluir != null)
                  {
                      EnderecosCadastrados.Remove(EndExcluir);
                  }

              }

          }
          */
    }
}
