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
            if (VerificarItens() == true)
            {
                Endereco end = SalvarEndereco(txtRua.Text, int.Parse(txtNumero.Text.Trim()), txtBairro.Text, txtComplemento.Text, txtReferencia.Text);
                Cliente clinovo = SalvarCliente(txtNome.Text, txtCPF.Text.Trim().Replace("-", "").Replace("(", "").Replace(")", ""), txtTelefone.Text.Trim().Replace("-", "").Replace("(", "").Replace(")", ""), end.EnderecoID);
                ClienteController.SalvarCliente(clinovo);
                MessageBox.Show("Cliente salvo com sucesso");
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
            cli.EnderecoID = ID;

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

        private bool VerificarItens()
        {
            string caracter = txtNumero.Text.Substring(0, 1);
            string verifica = "^[0-9]";

            if(!Regex.IsMatch(txtNome.Text, @"^[a-zA-Z]+$") || (txtNome.Text==null))
            {
                MessageBox.Show("ERRO, digite apenas caracter");
                return false;
            }
            else if((!Regex.IsMatch(caracter, verifica) || txtCPF.Text.Length.Equals(11) == false) || (txtCPF.Text == null))
            {
                MessageBox.Show("Erro ! Digite 11 dígitos no CPF e apenas números");
                return false;
            }
            else if(!Regex.IsMatch(txtTelefone.Text, verifica) || (txtTelefone.Text == null))
            {
                return false;
            }else if((txtRua.Text == null))
            {
                return false;
            }else if(!Regex.IsMatch(txtNumero.Text, verifica) || (txtNumero.Text == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}