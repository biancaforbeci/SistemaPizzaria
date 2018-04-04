using System;

using System.Windows;

using System.Data;

using System.Data.SqlClient;

using System.Configuration;
using Controllers;

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
            ClienteController cc = new ClienteController();
            cc.SalvarCliente(txtNome.Text, txtCPF.Text, txtTelefone.Text);
            EnderecoController ee = new EnderecoController();
            ee.SalvarEndereco(txtRua.Text,txtNumero.Text,txtBairro.Text,txtComplemento.Text,txtReferencia.Text);

        }   
    }
}
