using Controllers;
using Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditarCliente.xaml
    /// </summary>
    public partial class EditarCliente : Window
    {
        private Cliente cliEdicao;

        public EditarCliente()
        {
            InitializeComponent();            
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarItens() == true)
            {
                EditarEndereco(txtRua.Text, int.Parse(txtNumero.Text), txtBairro.Text, txtComplemento.Text, txtReferencia.Text, cliEdicao.EnderecoID);
                EnviarClienteEditado(txtNome.Text, txtCPF.Text.Replace("-", "").Replace("(", "").Replace(")", ""), txtTelefone.Text.Replace("-", "").Replace("(", "").Replace(")", ""));
                MessageBox.Show("Cliente editado");
                VerificaCadastroPizzas();
            }            
        }

        private void VerificaCadastroPizzas()
        {
            List<Pizza> list = PizzaController.ListarTodasPizzas();

            if (list != null)
            {
                FazerPedido pedido = new FazerPedido();
                pedido.MostrarCliente(cliEdicao.ClienteID);
                this.Close();
                pedido.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nenhuma pizza cadastrada");
                CadastroPizzas tela = new CadastroPizzas();
                this.Close();
                tela.ShowDialog();
            }
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
        }

        public void EditarNome(Cliente cli)
        {
            txtNome.Text = cli.Nome;
            txtCPF.Text = cli.Cpf;
            txtTelefone.Text = cli.Telefone;
            txtRua.Text = cli._Endereco.Rua;
            txtNumero.Text = Convert.ToString(cli._Endereco.Numero);
            txtBairro.Text = cli._Endereco.Bairro;
            txtComplemento.Text = cli._Endereco.Complemento;
            txtReferencia.Text = cli._Endereco.Referencia;
            cliEdicao = cli;
        }

        private void EnviarClienteEditado(string Nome, string CPF, string Telefone)
        {
            Cliente cli = new Cliente();
            cli.Nome = Nome;
            cli.Cpf = CPF;
            cli.Telefone = Telefone;

            ClienteController.EditarCliente(cliEdicao.ClienteID,cli);            
        }

        private Endereco EditarEndereco(string Rua, int Num, string Bairro, string Compl, string Refe,int id)
        {
            Endereco end = new Endereco();
            end.Rua = Rua;
            end.Numero = Num;
            end.Bairro = Bairro;
            end.Complemento = Compl;
            end.Referencia = Refe;

            EnderecoController.EditarEndereco(id,end);

            return end;
        }

        private bool VerificarItens()
        {
            string caracter = txtNumero.Text.Substring(0, 1);
            string verifica = "^[0-9]";

            if (!Regex.IsMatch(txtNome.Text, @"^[a-zA-Z]+$") || (txtNome.Text == null))
            {
                MessageBox.Show("ERRO, digite apenas caracter");
                return false;
            }
            else if ((!Regex.IsMatch(caracter, verifica) || txtCPF.Text.Length.Equals(11) == false) || (txtCPF.Text == null))
            {
                MessageBox.Show("Erro ! Digite 11 dígitos no CPF e apenas números");
                return false;
            }
            else if (!Regex.IsMatch(txtTelefone.Text, verifica) || (txtTelefone.Text == null))
            {
                return false;
            }
            else if ((txtRua.Text == null))
            {
                return false;
            }
            else if (!Regex.IsMatch(txtNumero.Text, verifica) || (txtNumero.Text == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnLimparCampos_Click(object sender, RoutedEventArgs e)
        {
            txtNome.Text = "";
            txtCPF.Text = "";
            txtTelefone.Text = "";
            txtRua.Text = "";
            txtNumero.Text = "";
            txtBairro.Text = "";
            txtComplemento.Text = "";
            txtReferencia.Text = "";
        }
    }
}
