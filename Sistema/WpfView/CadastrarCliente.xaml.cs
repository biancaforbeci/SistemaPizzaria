using System;

using System.Windows;

using System.Data;

using System.Data.SqlClient;

using System.Configuration;
using Controllers;
using System.Text.RegularExpressions;
using Models;

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
                Endereco end = SalvarEndereco(txtRua.Text, int.Parse(txtNumero.Text.Trim()), txtBairro.Text, txtComplemento.Text, txtReferencia.Text);
                Cliente clinovo = SalvarCliente(txtNome.Text, txtCPF.Text.Trim(), txtTelefone.Text.Trim(), end.EnderecoID);
                ClienteController.SalvarCliente(clinovo);
            }


        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
        }

        private Cliente SalvarCliente(string Nome, string CPF, string Telefone, int ID)
        {
            Cliente cli = new Cliente();
            cli.Nome = Nome;
            cli.Cpf = CPF;
            cli.Telefone = Telefone;
            cli.Endereco_ID = ID;
            return cli;
        }

        private Endereco SalvarEndereco(string Rua, int Num, string Bairro, string Compl, string Refe)
        {
            Endereco end = new Endereco();
            end.Rua = Rua;
            end.Numero = Num;
            end.Bairro = Bairro;
            end.Complemento = Compl;
            end.Referencia = Refe;

            EnderecoController.SalvarEndereco(end);

            return end;
        }

    }
}
