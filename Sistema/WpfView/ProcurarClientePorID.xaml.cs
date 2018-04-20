using Controllers;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfView
{
    /// <summary>
    /// Interaction logic for ProcurarClientePorID.xaml
    /// </summary>
    public partial class ProcurarClientePorID : Window
    {
        private Cliente cliEditar = null;

        public ProcurarClientePorID()
        {
            InitializeComponent();
            
        }       

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
        }        

        private void btnProcurar_Click(object sender, RoutedEventArgs e)
        {
            string caracter = txtID.Text.Substring(0, 1);
            string verifica = "^[0-9]";

            if ((Regex.IsMatch(caracter, verifica) || (txtID.Text != null)))
            {
                Cliente cli = ClienteController.PesquisarPorID(int.Parse(txtID.Text));
                cliEditar = cli;
                if (cli != null)
                {                    
                    blockID.Text = Convert.ToString(cli.ClienteID);
                    txtNome.Text = cli.Nome;
                    txtCPF.Text = cli.Cpf;
                    txtTelefone.Text = cli.Telefone;
                    txtRua.Text = cli._Endereco.Rua;
                    txtNumero.Text = Convert.ToString(cli._Endereco.Numero);
                    txtBairro.Text = cli._Endereco.Bairro;
                    txtComplemento.Text = cli._Endereco.Complemento;
                    txtReferencia.Text = cli._Endereco.Referencia;
                }
                else
                {
                    MessageBox.Show("ID não encontrado");
                }
            }
            else
            {
                MessageBox.Show("Campo inválido, digite apenas números.");
            }

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (cliEditar!=null)
            {
                EditarCliente tela = new EditarCliente();
                tela.EditarNome(ClienteController.PesquisarPorID(int.Parse(txtID.Text)));
                this.Close();
                tela.ShowDialog();
            }
            else
            {
                MessageBox.Show("Digite um ID válido");
            }

        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (cliEditar != null)
            {
                FazerPedido tela = new FazerPedido();
                tela.MostrarCliente(ClienteController.PesquisarPorID(int.Parse(txtID.Text)).ClienteID);
                this.Close();
                tela.ShowDialog();
            }
            else
            {
                MessageBox.Show("Digite um ID válido");
            }
            
        }
    }
}

