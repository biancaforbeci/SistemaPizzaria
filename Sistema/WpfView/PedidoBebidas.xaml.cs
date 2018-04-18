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
            MostrarGrid();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            this.Close();
            w.ShowDialog();
        }

        public void MostrarCliente(int id)
        {
            Cliente cli = ClienteController.PesquisarPorID(id);
            blockCliente.Text = cli.Nome;
            blockTelefone.Text = cli.Telefone;
            clientePedido = cli;
        }

        private void MostrarGrid()
        {
            List<Pizza> list = PizzaController.ListarTodasPizzas();

            if (list != null)
            {
                gridPizza.ItemsSource = list;
            }
            else
            {
                MessageBox.Show("Não foi cadastrado nenhuma pizza");
                PossuiPizzasCadastradas = false;
                CadastroPizzas tela = new CadastroPizzas();
                this.Close();
                tela.ShowDialog();
            }
        }

        private void gridPizza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridPizzasEscolhidas.Items.Count < 2 && qtdMaxPizza == 2)
            {
                SalvandoTabelaEscolhidos();
            }
            else if (gridPizzasEscolhidas.Items.Count < 3 && qtdMaxPizza == 3)
            {
                SalvandoTabelaEscolhidos();
            }
            else if (gridPizzasEscolhidas.Items.Count < 4 && qtdMaxPizza == 4)
            {
                SalvandoTabelaEscolhidos();
            }
            else
            {
                MessageBox.Show("Quantidade de pizzas excedidas");
            }
        }

        private void SalvandoTabelaEscolhidos()
        {
            Pizza pizzaEscolhida = ((Pizza)gridPizza.SelectedItem);
            SalvarPedido(pizzaEscolhida);
            valorTotal += ((Pizza)gridPizza.SelectedItem).Preco;
            blockValorTotal.Text = Convert.ToString(valorTotal);
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Confirmar pedido ?", "Confirma Pedido", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SalvandoNaTabelaPedidos();
                MessageBox.Show("Pedido finalizado");
                MainWindow tela = new MainWindow();
                this.Close();
                tela.ShowDialog();
            }
        }

        private void SalvarPedido(Pizza pizza)
        {
            ClientesPizzas novo = new ClientesPizzas();
            novo.ClienteID = clientePedido.ClienteID;
            novo.PizzaID = pizza.PizzaID;
            novo.Preco = pizza.Preco;
            DateTime data = DateTime.Now;
            novo.Data = data;
            ClientesPizzasController.SalvarItem(novo);
            MostrarGridPizzasEscolhidas();
        }

        private void MostrarGridPizzasEscolhidas()
        {
            List<ClientesPizzas> list = ClientesPizzasController.PesquisarClientePedidos(clientePedido.ClienteID);
            gridPizzasEscolhidas.ItemsSource = list;
        }

        private void SalvandoNaTabelaPedidos()
        {
            List<ClientesPizzas> list = ClientesPizzasController.PesquisarClientePedidos(clientePedido.ClienteID);
            Pedido novoPed = new Pedido();

            foreach (var item in list)
            {
                novoPed.Status = "EM PRODUÇÃO";
                novoPed.ClientesPizzasID = item.ClientesPizzasID;
                novoPed.NumPedido = GerarNumPedido();
                novoPed.ValorTotal = double.Parse(blockValorTotal.Text);
                novoPed.Tamanho_Pizza = TamPizza;
                PedidoController.SalvarPedido(novoPed);
            }
        }
        private void gridPizzasEscolhidas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridPizzasEscolhidas.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Confirma a exclusão do item " + ((ClientesPizzas)gridPizzasEscolhidas.SelectedItem).ClientesPizzasID + " ?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        int id = ((ClientesPizzas)gridPizzasEscolhidas.SelectedItem).ClientesPizzasID;
                        ClientesPizzasController.ExcluirSelecao(id);
                        MessageBox.Show("Item excluído com sucesso");
                        valorTotal -= ((ClientesPizzas)gridPizzasEscolhidas.SelectedItem).Preco;
                        blockValorTotal.Text = Convert.ToString(valorTotal);
                        MostrarGrid();
                        MostrarGridPizzasEscolhidas();
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("ERRO: " + erro);
                    }
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (PossuiPizzasCadastradas == true)
            {
                MainWindow tela = new MainWindow();
                if (MessageBox.Show("Deseja cancelar pedido ?", "Cancelar pedido", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ClientesPizzasController.ExcluirPedidosCliente(clientePedido.ClienteID);
                }
            }
        }

    }
}
