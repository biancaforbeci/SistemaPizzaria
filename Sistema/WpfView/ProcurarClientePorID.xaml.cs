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
    /// Interaction logic for ProcurarClientePorID.xaml
    /// </summary>
    public partial class ProcurarClientePorID : Window
    {
        public ProcurarClientePorID()
        {
            InitializeComponent();
            
        }

        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
             List<Cliente> dt = ClienteController.PesquisaPorIDLista(1);
             //List<Cliente> dt = ClienteController.ListarTodosClientes();
            if (dt!=null)
            {
                GridMostrar.ItemsSource = dt;
            }                        
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            string caracter = txtID.Text.Substring(0, 1);
            string verifica = "^[0-9]";

            if ((Regex.IsMatch(caracter, verifica) || (txtID.Text != null)))
            {
                List<Cliente> cli = ClienteController.PesquisaPorIDLista(int.Parse(txtID.Text));
                if (cli!=null)
                {
                    foreach (var c in cli)
                    {
                        string rua = c._Endereco.Rua;
                    }
                    GridMostrar.ItemsSource = cli;
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

        private void GridMostrar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridMostrar.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Deseja editar o cliente de nome: " + ((Cliente)GridMostrar.SelectedItem).Nome + " ?", "Editar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {      
                        Cliente cli = ((Cliente)GridMostrar.SelectedItem);
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
                    FazerPedido pedido = new FazerPedido();
                    Cliente cli = ((Cliente)GridMostrar.SelectedItem);
                    pedido.MostrarCliente(cli);
                    this.Close();
                    pedido.ShowDialog();
                }
            }
        }

    }
}

