using System;

using System.Windows;

using System.Data;

using System.Data.SqlClient;

using System.Configuration;

namespace WpfView
{
    /// <summary>
    /// Interaction logic for CadastrarCliente.xaml
    /// </summary>
    public partial class CadastrarCliente : Window
    {

        SqlConnection conn = new SqlConnection (@"Data Source=SAMSUNG\MSSQL;Initial Catalog=systempizza;Integrated Security=True");
        
        public CadastrarCliente()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {

            string sql = "INSERT INTO Clientes(Nome, CPF, Telefone) VALUES (@Nome, @CPF, @Telefone)";
            try
                {
               
                SqlCommand comando = new SqlCommand(sql, conn);

                comando.Parameters.Add(new SqlParameter("@Nome", this.txtNome.Text));
                comando.Parameters.Add(new SqlParameter("@CPF", this.txtCPF.Text));
                comando.Parameters.Add(new SqlParameter("@Telefone", this.txtTelefone.Text));
                
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
                MessageBox.Show("Cadastro realizado com sucesso");
                }
           
        }
    }
}
