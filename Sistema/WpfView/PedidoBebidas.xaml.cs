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
        private int numReferencia = 0;
        private List<ClientesPizzas> list = null;
        private int referenciaButton= 0;

        public PedidoBebidas()
        {
            InitializeComponent();
            MostrarGrid();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            referenciaButton = 1;
            ClientesPizzasController.ExcluirPedidosCliente(clientePedido.ClienteID,numReferencia);
            ClientesBebidasController.ExcluirPedidosCliente(clientePedido.ClienteID,numReferencia);
            MainWindow w = new MainWindow();
            this.Close();
            w.ShowDialog();
        }

        public void MostrarClienteParteBebidas(Cliente cli, double total, int num, List<ClientesPizzas> listPizzas, int numRefe)
        {
            blockCliente.Text = cli.Nome;
            blockTelefone.Text = cli.Telefone;
            clientePedido = cli;
            blockValorTotal.Text = Convert.ToString(total.ToString("C2"));
            numReferencia = numRefe;
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
            SalvarEscolha(bebidaEscolhida);
            valorTotal += ((Bebida)gridBebida.SelectedItem).Preco;
            blockValorTotal.Text = Convert.ToString(valorTotal.ToString("C2"));
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            referenciaButton = 2;
            if (MessageBox.Show("Confirmar pedido ?", "Confirma Pedido", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SalvarTabelaPedidos();
                MessageBox.Show("Pedido finalizado");
                MainWindow tela = new MainWindow();
                this.Close();
                tela.ShowDialog();
            }
        }

        private void SalvarEscolha(Bebida bebida)
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

        private void SalvarTabelaPedidos()
        {
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
            List<ClientesBebidas> lista = ClientesBebidasController.PesquisarClientePedidos(clientePedido.ClienteID, numReferencia);
            SalvarTabelaPedidoBebidas(pedidoID, lista);
        }

        private void SalvarTabelaPedidoBebidas(int pedidoID, List<ClientesBebidas> listaBebidas)
        {
            PedidoBebidas pedidoBebidas = new PedidoBebidas();            
            foreach (var item in list)
            {
                pedidoBebidas.NumeroPedidoID = pedidoID;
                novo.ClientesPizzasID = item.ClientesPizzasID;
            }
            PedidoBebidasController.SalvarPedido(novo);
        }

        private void gridBebidasEscolhidas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridBebidasEscolhidas.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Confirma a exclusão do item " + ((ClientesBebidas)gridBebidasEscolhidas.SelectedItem)._Bebida.Nome + " ?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        int id = ((ClientesBebidas)gridBebidasEscolhidas.SelectedItem).ClientesBebidasID;
                        ClientesBebidasController.ExcluirSelecao(id);
                        MessageBox.Show("Item excluído com sucesso");
                        valorTotal -= ((ClientesBebidas)gridBebidasEscolhidas.SelectedItem).Preco;
                        blockValorTotal.Text = Convert.ToString(valorTotal.ToString("C2"));
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
            if (referenciaButton==0)
            {
                if (MessageBox.Show("Deseja cancelar pedido ?", "Cancelar pedido", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ClientesPizzasController.ExcluirPedidosCliente(clientePedido.ClienteID);
                    Environment.Exit(0);
                }
            }else if (referenciaButton == 1)
            {
                if (MessageBox.Show("Deseja cancelar pedido ?", "Cancelar pedido", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ClientesPizzasController.ExcluirPedidosCliente(clientePedido.ClienteID);
                }
            }              
        }

    }
}
