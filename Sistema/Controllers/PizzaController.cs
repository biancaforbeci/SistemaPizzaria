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
    public class PizzaController
    {

        SqlConnection conn = new SqlConnection(@"Data Source=SAMSUNG\MSSQL;Initial Catalog=systempizza;Integrated Security=True");

        public void CadastrarPizza(string txtNome, string txtIngredientes, double txtPreco)
        {

            string sql = "INSERT INTO Pizzas(Nome, Ingredientes, Preco) VALUES (@Nome, @Ingredientes, @Preco)";
            try
            {

                SqlCommand comando = new SqlCommand(sql, conn);


                comando.Parameters.Add(new SqlParameter("@Nome", txtNome));
                comando.Parameters.Add(new SqlParameter("@Ingredientes", txtIngredientes));
                comando.Parameters.Add(new SqlParameter("@Preco", txtPreco));

                conn.Open();

                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro de pizza concluído");
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
                    string sql = "SELECT ID_Pizza,Nome, Ingredientes, Preco FROM Pizzas";
                    conn.Open();
                    SqlCommand comando = new SqlCommand(sql, conn);
                    comando.ExecuteNonQuery();

                    SqlDataAdapter Da = new SqlDataAdapter(comando);
                    Da.SelectCommand = comando;

                    DataTable Dt = new DataTable("Pizzas");
                    Da.Fill(Dt);

                    conn.Close();
                    return Dt;
                }
                catch (Exception erro)
                {
                    throw erro;
                }
            }

            public void UpdateTable()
             {
                 
             }

        }
    }
