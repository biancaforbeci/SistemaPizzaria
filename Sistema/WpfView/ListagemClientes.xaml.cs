using Controllers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for ListagemClientes.xaml
    /// </summary>
    public partial class ListagemClientes : Window
    {
        public ListagemClientes()
        {
            InitializeComponent();
            MostrarClientes();
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
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
                    pedido.MostrarCliente(((Cliente)GridMostrar.SelectedItem).ClienteID);
                    this.Close();
                    pedido.ShowDialog();
                }
            }
        }

            private void MostrarClientes()
            {
                List<Cliente> dt = ClienteController.ListarTodosClientes();
                if (dt != null)
                {
                    GridMostrar.ItemsSource = dt;
                }

            }

        }
    }
}
