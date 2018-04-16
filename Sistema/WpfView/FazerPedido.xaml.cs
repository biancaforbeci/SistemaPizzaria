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
    /// Interaction logic for FazerPedido.xaml
    /// </summary>
    public partial class FazerPedido : Window
    {
        private static double valorTotal = 0;
        private static Cliente clientePedido = null;

        public FazerPedido()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            this.Close();
            w.ShowDialog();
        }

        public void MostrarCliente(Cliente cli)
        {
            blockCliente.Text= cli.Nome;
            blockTele.Text = cli.Telefone;
            clientePedido = cli;
        }

        private void btnMostrar_Click(object sender, RoutedEventArgs e)
        {
            List<Pizza> list = PizzaController.ListarTodasPizzas();

            if (list != null)
            {
                gridPizza.ItemsSource = list;
            }
            else
            {
                MessageBox.Show("Não foi cadastrado nenhuma pizza");
            }
        }

        private void gridPizza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double valor = ((Pizza)gridPizza.SelectedItem).Preco;
            valorTotal += valor;
            blockValorTotal.Text = Convert.ToString(valorTotal);
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result=MessageBox.Show("Confirmar pedido ?", "Confirma Pedido", MessageBoxButton.YesNo, MessageBoxImage.Question);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PedidoBebidas bebidas = new PedidoBebidas();
            bebidas.ClientePedido(clientePedido, valorTotal);
            this.Close();
            bebidas.ShowDialog();
        }
    }
}
