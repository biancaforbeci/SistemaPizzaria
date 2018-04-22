using Controllers;
using Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ProcurarCliente.xaml
    /// </summary>
    public partial class ProcurarCliente : Window
    {
        public ProcurarCliente()
        {
            InitializeComponent();
        }

        private void ProcuraID(object sender, RoutedEventArgs e)
        {
            ProcurarClientePorID pID = new ProcurarClientePorID();
            this.Close();
            pID.ShowDialog();
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
        }

        private void btnProcura_Click(object sender, RoutedEventArgs e)
        {

            string caracter = txtTelefone.Text.Substring(0, 1);
            string verifica = "^[0-9]";                   

            if ((txtTelefone.Text.Trim() != "") || (Regex.IsMatch(caracter,verifica)))
            {
                List<Cliente> cli = ClienteController.PesquisarPorTelefone(txtTelefone.Text);
                if (cli != null)
                {
                    gridCliente.ItemsSource = cli;
                }
                else
                {
                   if (MessageBox.Show("Cliente não encontrado, deseja cadastrar novo cliente ?","Cliente não encontrado",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        CadastrarCliente cad = new CadastrarCliente();
                        this.Close();
                        cad.ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show("Só é aceito campo númerico e que não esteja vazio.");
            }
        }

        private void MensagemErro()
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Telefone não cadastrado ! Deseja cadastrar cliente ?", "Cliente não encontrado", MessageBoxButton.YesNo, MessageBoxImage.Error);
            if (result == MessageBoxResult.Yes)
            {
                CadastrarCliente ccli = new CadastrarCliente();
                this.Close();
                ccli.ShowDialog();
            }            
        }

        private void gridCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridCliente.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Deseja editar o cliente de nome: " + ((Cliente)gridCliente.SelectedItem).Nome + " ?", "Editar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        Cliente cli = ((Cliente)gridCliente.SelectedItem);
                        EditarCliente edit = new EditarCliente();
                        edit.EditarNome(cli);
                        this.Close();
                        edit.ShowDialog();
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("ERRO: " + erro);
                    }
                }
                else
                {
                    VerificaCadastroPizzas();                
                }
            }

        }

        private void VerificaCadastroPizzas()
        {
            List<Pizza> list= PizzaController.ListarTodasPizzas();
            
            if (list != null)
            {
                FazerPedido pedido = new FazerPedido();
                pedido.MostrarCliente(((Cliente)gridCliente.SelectedItem).ClienteID);
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
    }
}
