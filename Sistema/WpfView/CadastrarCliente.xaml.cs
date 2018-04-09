using System;

using System.Windows;

using System.Data;

using System.Data.SqlClient;

using System.Configuration;
using Controllers;
using System.Text.RegularExpressions;

namespace WpfView
{
    /// <summary>
    /// Interaction logic for CadastrarCliente.xaml
    /// </summary>
    public partial class CadastrarCliente : Window
    {

              
        public CadastrarCliente()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            bool result = true;
            string caracter = txtNumero.Text.Substring(0, 1);
            string verifica = "^[0-9]";

            if ((Regex.IsMatch(caracter, verifica) && txtCPF.Text.Length.Equals(11) == true))
            {
              
            }
            else
            {
                MessageBox.Show("Erro !");
                result = false;
                MainWindow m = new MainWindow();
                m.ShowDialog();
            }

            if (result == true)
            {
                EnderecoController ee = new EnderecoController();
                ee.SalvarEndereco(txtRua.Text, txtNumero.Text.Trim(), txtBairro.Text, txtComplemento.Text, txtReferencia.Text);
                ClienteController cc = new ClienteController();
                cc.SalvarCliente(txtNome.Text, txtCPF.Text.Trim(), txtTelefone.Text.Trim());
            }

        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
        }
    }
}
