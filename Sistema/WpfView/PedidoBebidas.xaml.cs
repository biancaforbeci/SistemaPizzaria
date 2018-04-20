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
        private double valorTotal = 0;
        private Cliente clientePedido = null;
        private int numPedido;
        private List<ClientesPizzas> list = null;

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

        public void MostrarClienteParteBebidas(Cliente cli, double total, int num, List<ClientesPizzas> listPizzas)
        {
            blockCliente.Text = cli.Nome;
            blockTelefone.Text = cli.Telefone;
            clientePedido = cli;
            blockValorTotal.Text = Convert.ToString(total);
            valorTotal = total;
            numPedido = num;
            list = listPizzas;
        }

        private void MostrarGrid()
        {
            List<Bebida> list = BebidasController.ListarTodasBebidas();

            if (list != null)
            {
                gridBebida.ItemsSource = list;
            }
            else
            {
                MessageBox.Show("Não foi cadastrado nenhuma bebida");
                Bebidas tela = new Bebidas();
                this.Close();
                tela.ShowDialog();
            }
        }

        private void gridBebida_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SalvandoTabelaEscolhidos();
        }

        private void SalvandoTabelaEscolhidos()
        {
            Bebida bebidaEscolhida = ((Bebida)gridBebida.SelectedItem);
            SalvarPedido(bebidaEscolhida);
            valorTotal += ((Bebida)gridBebida.SelectedItem).Preco;
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

        private void SalvarPedido(Bebida bebida)
        {
            ClientesBebidas novo = new ClientesBebidas();
            novo.ClienteID = clientePedido.ClienteID;
            novo.BebidaID = bebida.BebidaID;
            novo.Preco = bebida.Preco;
            DateTime data = DateTime.Now;
            novo.Data = data;
            ClientesBebidasController.SalvarItem(novo);
            MostrarGridBebidasEscolhidas();
        }

        private void MostrarGridBebidasEscolhidas()
        {
            List<ClientesBebidas> list = ClientesBebidasController.PesquisarClientePedidos(clientePedido.ClienteID);
            gridBebidasEscolhidas.ItemsSource = list;
        }

        private void SalvandoNaTabelaPedidos()
        {
            SalvarPizzasTabelaPedidos();
            List<ClientesBebidas> listaBebidas = ClientesBebidasController.PesquisarClientePedidos(clientePedido.ClienteID);
            Pedido novoPed = new Pedido();

            foreach (var item in listaBebidas)
            {
                novoPed.Status = "EM PRODUÇÃO";
                novoPed.ClientesProdutosEscolhidosID = item.ClientesBebidasID;
                novoPed.NumPedido = numPedido;
                novoPed.ValorTotal = double.Parse(blockValorTotal.Text);
                PedidoController.SalvarPedido(novoPed);
            }
        }

        private void SalvarPizzasTabelaPedidos()
        {
            Pedido novoPed = new Pedido();

            foreach (var item in list)
            {
                novoPed.Status = "EM PRODUÇÃO";
                novoPed.ClientesProdutosEscolhidosID = item.ClientesPizzasID;
                novoPed.NumPedido = numPedido;
                novoPed.ValorTotal = double.Parse(blockValorTotal.Text);
                PedidoController.SalvarPedido(novoPed);
            }
        }

        private void gridBebidasEscolhidas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridBebidasEscolhidas.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Confirma a exclusão do item " + ((ClientesBebidas)gridBebidasEscolhidas.SelectedItem).ClientesBebidasID + " ?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        int id = ((ClientesBebidas)gridBebidasEscolhidas.SelectedItem).ClientesBebidasID;
                        ClientesBebidasController.ExcluirSelecao(id);
                        MessageBox.Show("Item excluído com sucesso");
                        valorTotal -= ((ClientesBebidas)gridBebidasEscolhidas.SelectedItem).Preco;
                        blockValorTotal.Text = Convert.ToString(valorTotal);
                        MostrarGrid();
                        MostrarGridBebidasEscolhidas();
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
            
                MainWindow tela = new MainWindow();
                if (MessageBox.Show("Deseja cancelar pedido ?", "Cancelar pedido", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ClientesPizzasController.ExcluirPedidosCliente(clientePedido.ClienteID);
                }
            
        }

    }
}
