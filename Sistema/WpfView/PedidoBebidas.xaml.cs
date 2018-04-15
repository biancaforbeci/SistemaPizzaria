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
    /// Interaction logic for PedidoBebidas.xaml
    /// </summary>
    public partial class PedidoBebidas : Window
    {
        private static double valorTotal = 0;
        private static Cliente clientePedido = null;

        public PedidoBebidas()
        {
            InitializeComponent();
        }

        public void ClientePedido(Cliente cli, double valor)
        {
            blockCliente.Text = cli.Nome;
            blockTele.Text = cli.Telefone;
            valorTotal = valor;
            clientePedido = cli;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            this.Close();
            w.ShowDialog();
        }

        public void MostrarCliente(Cliente cli)
        {
            blockCliente.Text = cli.Nome;
            blockTele.Text = cli.Telefone;
        }

        private void btnMostrar_Click(object sender, RoutedEventArgs e)
        {
            List<Bebida> list = BebibasController.ListarTodasBebidas();

            if (list != null)
            {
                gridBebidas.ItemsSource = list;
            }
            else
            {
                MessageBox.Show("Não foi cadastrado nenhuma pizza");
            }
        }

        private void gridPizza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double valor = ((Bebida)gridBebidas.SelectedItem).Preco;
            valorTotal += valor;
            blockValorTotal.Text = Convert.ToString(valorTotal);
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Confirmar pedido ?", "Confirma Pedido", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SalvarPedido();
            }
        }

        private void SalvarPedido()
        {
            Pedido novoPedido = new Pedido();
            novoPedido.ValorTotal = valorTotal;
            // novoPedido.Produto_ID=
        }
    }
}
