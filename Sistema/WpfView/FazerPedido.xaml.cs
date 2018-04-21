using Controllers;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
        private double valorTotal = 0;
        private Cliente clientePedido;
        private int qtdMaxPizza = 0;
        private string TamPizza;
        private int numPedido = 0;
        private int referenciaButton = 0;
        private int NumReferencia=0;

        public FazerPedido()
        {
            InitializeComponent();
            MostrarGrid();
            blockValorTotal.Text = valorTotal.ToString("C2");
            GerarNumReferencia();
        }

        private void GerarNumReferencia()
        {
            int num = ClientesPizzasController.RetornaUltimo();
            
            if( num != -1)
            {
                NumReferencia = num + 1;
            }
            else
            {
                NumReferencia = 0;
            }
        }
        
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            referenciaButton = 1;
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
            gridPizza.ItemsSource = list;
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
            SalvarPizzaEscolhida(pizzaEscolhida);
            valorTotal += ((Pizza)gridPizza.SelectedItem).Preco;
            blockValorTotal.Text = Convert.ToString(valorTotal.ToString("C2"));
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            referenciaButton = 2;
            if (MessageBox.Show("Confirmar pedido ?", "Confirma Pedido", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SalvandoNaTabelaPedidos();
                MessageBox.Show("Pedido finalizado");
                MainWindow tela = new MainWindow();
                this.Close();
                tela.ShowDialog();
            }
        }

        private void SalvarPizzaEscolhida(Pizza pizza)
        {
            ClientesPizzas novo = new ClientesPizzas();
            novo.ClienteID = clientePedido.ClienteID;
            novo.PizzaID = pizza.PizzaID;
            novo.Preco = pizza.Preco;
            novo.Tamanho_Pizza = TamPizza;
            novo.NumReferencia = NumReferencia;
            ClientesPizzasController.SalvarItem(novo);
            MostrarGridPizzasEscolhidas();
        }

        private void MostrarGridPizzasEscolhidas()
        {
            List<ClientesPizzas> list = ClientesPizzasController.PesquisarClientePedidos(clientePedido,NumReferencia);
            gridPizzasEscolhidas.ItemsSource = list;
        }

        private void SalvandoNaTabelaPedidos()
        {
            List<ClientesPizzas> list = ClientesPizzasController.PesquisarClientePedidos(clientePedido,NumReferencia);
            Pedido novoPed = new Pedido();            
            novoPed.Status = "EM PRODUÇÃO";
            DateTime data = DateTime.Now;
            novoPed.Data = data;
            novoPed.ValorTotal = valorTotal;
            PedidoController.SalvarPedido(novoPed);
            SalvarTabelaPedidoPizzas(novoPed.NumeroPedidoID,list);            
        }

        private void SalvarTabelaPedidoPizzas(int pedidoID, List<ClientesPizzas> list)
        {
            PedidoPizzas novo = new PedidoPizzas();
            foreach (var item in list)
            {
                novo.NumeroPedidoID = pedidoID;
                novo.ClientesPizzasID = item.ClientesPizzasID;
            }
            PedidoPizzasController.SalvarPedido(novo);
        }

        private void Bebidas_Click(object sender, RoutedEventArgs e)
        {
            referenciaButton = 2;
            if (gridPizzasEscolhidas.Items.Count > 0)
            {
                PedidoBebidas bebidas = new PedidoBebidas();
                List<ClientesPizzas> list = ClientesPizzasController.PesquisarClientePedidos(clientePedido,NumReferencia);
                bebidas.MostrarClienteParteBebidas(clientePedido, valorTotal, numPedido, list, NumReferencia);
                this.Close();
                bebidas.ShowDialog();
            }
            else
            {
                MessageBox.Show("Escolha alguma pizza", "Erro", MessageBoxButton.OK);
            }            
        }        

        private void gridPizzasEscolhidas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridPizzasEscolhidas.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Confirma a exclusão do item " + ((ClientesPizzas)gridPizzasEscolhidas.SelectedItem)._Pizza.Nome + " ?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        int id = ((ClientesPizzas)gridPizzasEscolhidas.SelectedItem).ClientesPizzasID;
                        ClientesPizzasController.ExcluirSelecao(id);
                        MessageBox.Show("Item excluído com sucesso");
                        valorTotal -= ((ClientesPizzas)gridPizzasEscolhidas.SelectedItem).Preco;
                        blockValorTotal.Text = Convert.ToString(valorTotal.ToString("C2"));
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

        private void CheckBoxBroto_Checked(object sender, RoutedEventArgs e)
        {
            qtdMaxPizza = 2;
            checkMedia.IsEnabled = false;
            checkGigante.IsEnabled = false;
            checkGrande.IsEnabled = false;
            TamPizza = "Broto";
        }

        private void checkMedia_Checked(object sender, RoutedEventArgs e)
        {
            qtdMaxPizza = 3;
            checkBroto.IsEnabled = false;
            checkGigante.IsEnabled = false;
            checkGrande.IsEnabled = false;
            TamPizza = "Média";
        }

        private void checkGrande_Checked(object sender, RoutedEventArgs e)
        {
            qtdMaxPizza = 3;
            checkBroto.IsEnabled = false;
            checkMedia.IsEnabled = false;
            checkGigante.IsEnabled = false;
            TamPizza = "Média";
        }

        private void checkGigante_Checked(object sender, RoutedEventArgs e)
        {
            qtdMaxPizza = 4;
            checkBroto.IsEnabled = false;
            checkMedia.IsEnabled = false;
            checkGrande.IsEnabled = false;
            TamPizza = "Média";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (referenciaButton == 0)
            {
                if (MessageBox.Show("Pedido será cancelado", "Cancelar pedido", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                {
                    ClientesPizzasController.ExcluirPedidosCliente(clientePedido.ClienteID,NumReferencia);
                    Environment.Exit(0);
                }
            } else if (referenciaButton == 1)
            {
                if (MessageBox.Show("Pedido será cancelado", "Cancelar pedido", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                {
                    ClientesPizzasController.ExcluirPedidosCliente(clientePedido.ClienteID,NumReferencia);
                }
            }            
        }

        private void checkBroto_Unchecked(object sender, RoutedEventArgs e)
        {
            qtdMaxPizza = 0;
            checkMedia.IsEnabled = true;
            checkGigante.IsEnabled = true;
            checkGrande.IsEnabled = true;
            TamPizza =null;
        }

        private void checkMedia_Unchecked(object sender, RoutedEventArgs e)
        {
            qtdMaxPizza = 0;
            checkBroto.IsEnabled = true;
            checkGigante.IsEnabled = true;
            checkGrande.IsEnabled = true;
            TamPizza = null;
        }

        private void checkGrande_Unchecked(object sender, RoutedEventArgs e)
        {
            qtdMaxPizza = 0;
            checkBroto.IsEnabled = true;
            checkMedia.IsEnabled = true;
            checkGigante.IsEnabled = true;
            TamPizza = null;

        }

        private void checkGigante_Unchecked(object sender, RoutedEventArgs e)
        {
            qtdMaxPizza = 0;
            checkBroto.IsEnabled = true;
            checkMedia.IsEnabled = true;
            checkGrande.IsEnabled = true;
            TamPizza = null;
        }
    }
}
