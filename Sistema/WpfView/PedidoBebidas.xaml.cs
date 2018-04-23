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
    /// Interaction logic for PedidoBebidas.xaml
    /// </summary>
    public partial class PedidoBebidas : Window
    {
        private Decimal valorTotal = 0;
        private Cliente clientePedido = null;
        private int Pizzas;
        private int numRef;
        private List<ClientesPizzas> list = null;
        private int referenciaButton= 0;

        public PedidoBebidas()
        {
            InitializeComponent();
            MostrarGrid();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            referenciaButton = 1;
            ClientesBebidasController.ExcluirPedidosCliente(clientePedido.ClienteID, numRef);
            ClientesPizzasController.ExcluirPedidosCliente(clientePedido.ClienteID, numRef);
            MainWindow w = new MainWindow();
            this.Close();
            w.ShowDialog();
        }

        public void MostrarClienteBebidas(Cliente cli, Decimal total, int num, List<ClientesPizzas> listPizzas, int numRefe, int qtd)
        {
            blockCliente.Text = cli.Nome;
            blockTelefone.Text = cli.Telefone;
            clientePedido = cli;
            blockValorTotal.Text = Convert.ToString(total.ToString("C2"));
            numRef = numRefe;
            valorTotal = total;
            list = listPizzas;
            Pizzas = qtd;
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
            string verifica = "^[0-9]";

            if (txtQuantidadeBebida.Text == "" || (!Regex.IsMatch(txtQuantidadeBebida.Text.Substring(0, 1), verifica)))
            {
                MessageBox.Show("Escolha uma quantidade desse item e digite o valor númerico", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SalvandoTabelaEscolhidos();
                MostrarGridBebidasEscolhidas();
            }            
        }

        private void SalvandoTabelaEscolhidos()
        {
            Bebida bebidaEscolhida = ((Bebida)gridBebida.SelectedItem);
            SalvarEscolha(bebidaEscolhida);
            valorTotal += (((Bebida)gridBebida.SelectedItem).Preco*int.Parse(txtQuantidadeBebida.Text));
            blockValorTotal.Text = Convert.ToString(valorTotal.ToString("C2"));
        }

        private void btnConfirma_Click(object sender, RoutedEventArgs e)
        {
            referenciaButton = 2;
            
            if (gridBebidasEscolhidas.Items.Count > 0)
            {
                if (MessageBox.Show("Confirmar pedido ?", "Confirma Pedido", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int i = SalvarTabelaPedidos();
                    List<ClientesBebidas> lista = ClientesBebidasController.PesquisarClientePedidos(clientePedido.ClienteID, numRef);
                    SalvarTabelaPedidoBebidas(i, lista);
                    MessageBox.Show("Pedido finalizado");
                    MainWindow tela = new MainWindow();
                    this.Close();
                    tela.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Escolha uma bebida","Erro",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void SalvarTabelaPedidoBebidas(int ID, List<ClientesBebidas> listaBebida)
        {
            foreach (var item in listaBebida)
            {
                BebidasPedido novo = new BebidasPedido();
                novo.NumeroPedidoID = ID;
                novo.ClientesBebidasID = item.ClientesBebidasID;
                PedidoBebidasController.SalvarPedido(novo);
            }
        }

        private void SalvarEscolha(Bebida bebida)
        {
            ClientesBebidas cliBebidas = new ClientesBebidas();
            cliBebidas.ClienteID = clientePedido.ClienteID;
            cliBebidas.BebidaID = bebida.BebidaID;
            cliBebidas.NumReferencia = numRef;
            cliBebidas.QtdBebida = int.Parse(txtQuantidadeBebida.Text);
            ClientesBebidasController.SalvarItem(cliBebidas);
        }

        private void MostrarGridBebidasEscolhidas()
        {
             List<ClientesBebidas> list = ClientesBebidasController.PesquisarClientePedidos(clientePedido.ClienteID, numRef);
             gridBebidasEscolhidas.ItemsSource = list;
                      
        }

        private int SalvarTabelaPedidos()
        {
            Pedido novoPed = new Pedido();
            novoPed.Status = "EM PRODUÇÃO";
            DateTime data = DateTime.Now;
            novoPed.Data = data;
            novoPed.ValorTotal = valorTotal;
            novoPed.QtdPizzas = Pizzas;
            PedidoController.SalvarPedido(novoPed);
            SalvarTabelaPedidoPizzas(novoPed.NumeroPedidoID,list);
            return novoPed.NumeroPedidoID;
        }

        private void SalvarTabelaPedidoPizzas(int pedidoID, List<ClientesPizzas> list)
        {
            foreach (var item in list)
            {
                PedidoPizzas novo = new PedidoPizzas();
                novo.NumeroPedidoID = pedidoID;
                novo.ClientesPizzasID = item.ClientesPizzasID;
                PedidoPizzasController.SalvarPedido(novo);
            }
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
                        valorTotal -= ((ClientesBebidas)gridBebidasEscolhidas.SelectedItem)._Bebida.Preco;
                        blockValorTotal.Text = Convert.ToString(valorTotal.ToString("C2"));
                        ClientesBebidasController.ExcluirSelecao(id);
                        MessageBox.Show("Item excluído com sucesso");
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
            if (referenciaButton != 1 && referenciaButton != 2)
            {
                MessageBox.Show("Só é permitido cancelar pedido !!", "Não Permitido", MessageBoxButton.OK, MessageBoxImage.Stop);
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

    }
}
