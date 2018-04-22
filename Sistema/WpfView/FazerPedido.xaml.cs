﻿using Controllers;
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
        private int referenciaButton=0;
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
            ClientesPizzasController.ExcluirPedidosCliente(clientePedido.ClienteID, NumReferencia);
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
            }else if(qtdMaxPizza == 0)
            {
                MessageBox.Show("Escolha uma opção de tamanho antes de selecionar o sabor", "Erro", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else
            {
                MessageBox.Show("Quantidade de pizzas excedidas");
            }
        }

        private void CalculaValorTotal(Pizza pizzaEscolhida)
        {
            if (TamPizza.Contains("Broto"))
            {
                if (valorTotal == 0)
                {
                    valorTotal = pizzaEscolhida.PrecoBroto;
                }
                else
                {
                    valorTotal += pizzaEscolhida.PrecoBroto;
                }                
                
            } else if (TamPizza.Contains("Média")) 
            {
                if (valorTotal == 0)
                {
                    valorTotal = pizzaEscolhida.PrecoMedia;
                }
                else
                {
                    valorTotal += pizzaEscolhida.PrecoMedia;
                }
            }
            else if (TamPizza.Contains("Grande")) 
            {
                if (valorTotal == 0)
                {
                    valorTotal = pizzaEscolhida.PrecoGrande;
                }
                else
                {
                    valorTotal += pizzaEscolhida.PrecoGrande;
                }
            }
            else
            {
                if (valorTotal == 0)
                {
                    valorTotal = pizzaEscolhida.PrecoGigante;
                }
                else
                {
                    valorTotal += pizzaEscolhida.PrecoGigante;
                }
            }
        }

        private void SalvandoTabelaEscolhidos()
        {
            Pizza pizzaEscolhida = ((Pizza)gridPizza.SelectedItem);
            SalvarPizzaEscolhida(pizzaEscolhida);
            CalculaValorTotal(pizzaEscolhida);
            blockValorTotal.Text = Convert.ToString(valorTotal.ToString("C2"));
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            referenciaButton = 2;
            if(txtQuantidade.Text == "")
            {
                MessageBox.Show("Erro,coloque uma quantidade");
            }else if (gridPizzasEscolhidas.Items.Count > 0)
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
            else
            {
                MessageBox.Show("Escolha uma pizza e coloque uma quantidade.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SalvarPizzaEscolhida(Pizza pizza)
        {
            ClientesPizzas novo = new ClientesPizzas();
            novo.ClienteID = clientePedido.ClienteID;
            novo.PizzaID = pizza.PizzaID;
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
            MessageBox.Show("Num itens: " + list.Count);
            Pedido novoPed = new Pedido();            
            novoPed.Status = "EM PRODUÇÃO";
            DateTime data = DateTime.Now;
            novoPed.Data = data;
            novoPed.ValorTotal = valorTotal;
            novoPed.QtdPizzas = int.Parse(txtQuantidade.Text);
            PedidoController.SalvarPedido(novoPed);
            SalvarTabelaPedidoPizzas(novoPed.NumeroPedidoID,list);            
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

        private void Bebidas_Click(object sender, RoutedEventArgs e)
        {
            referenciaButton = 2;
            if(txtQuantidade.Text == "")
            {
                MessageBox.Show("Escolha uma quantidade de pizza.");
            }else if (gridPizzasEscolhidas.Items.Count > 0)
            {
                PedidoBebidas bebidas = new PedidoBebidas();
                List<ClientesPizzas> list = ClientesPizzasController.PesquisarClientePedidos(clientePedido,NumReferencia);
                bebidas.MostrarClienteBebidas(clientePedido, valorTotal, numPedido, list, NumReferencia,int.Parse(txtQuantidade.Text));
                this.Close();
                bebidas.ShowDialog();
            }
            else
            {
                MessageBox.Show("Escolha alguma pizza", "Erro", MessageBoxButton.OK,MessageBoxImage.Error);
            }            
        }        

        private void DiminuiValorTotal(ClientesPizzas pizzaTirada)
        {
            if (TamPizza.Contains("Broto"))
            {
                if (gridPizzasEscolhidas.Items.Count == 0)
                {
                    valorTotal = 0;
                }
                else
                {
                    valorTotal = valorTotal-pizzaTirada._Pizza.PrecoBroto;
                }            
            }
            else if (TamPizza.Contains("Média"))
            {
                if (gridPizzasEscolhidas.Items.Count == 0)
                {
                    valorTotal = 0;
                }
                else
                {
                    valorTotal = valorTotal  - pizzaTirada._Pizza.PrecoMedia;
                }
            }
            else if (TamPizza.Contains("Grande"))
            {
                if (gridPizzasEscolhidas.Items.Count == 0)
                {
                    valorTotal = 0;
                }
                else
                {
                    valorTotal = valorTotal - pizzaTirada._Pizza.PrecoGrande;
                }
            }
            else
            {
                if (gridPizzasEscolhidas.Items.Count == 0)
                {
                    valorTotal = 0;
                }
                else
                {
                    valorTotal = valorTotal  - pizzaTirada._Pizza.PrecoGigante;
                }
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
                        DiminuiValorTotal(((ClientesPizzas)gridPizzasEscolhidas.SelectedItem));
                        ClientesPizzasController.ExcluirSelecao(id);
                        MessageBox.Show("Item excluído com sucesso");                     
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
            TamPizza = "Grande";
        }

        private void checkGigante_Checked(object sender, RoutedEventArgs e)
        {
            qtdMaxPizza = 4;
            checkBroto.IsEnabled = false;
            checkMedia.IsEnabled = false;
            checkGrande.IsEnabled = false;
            TamPizza = "Gigante";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (referenciaButton != 1 && referenciaButton!=2)
            {
                MessageBox.Show("Só é permitido cancelar pedido, não feche a tela", "Não Permitido", MessageBoxButton.OK, MessageBoxImage.Stop);
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }            
        }

        private void ExcluirSelecaoAposMudarTamanho()
        {
            List<ClientesPizzas> listaSelecao = ClientesPizzasController.PesquisarClientePedidos(clientePedido, NumReferencia);
            foreach (var item in listaSelecao)
            {
                ClientesPizzasController.ExcluirSelecao(item.ClientesPizzasID);
            }
            MostrarGridPizzasEscolhidas();
            valorTotal = 0;
            blockValorTotal.Text=valorTotal.ToString("C2");
        }

        private void checkBroto_Unchecked(object sender, RoutedEventArgs e)
        {
            qtdMaxPizza = 0;
            ExcluirSelecaoAposMudarTamanho();
            checkMedia.IsEnabled = true;
            checkGigante.IsEnabled = true;
            checkGrande.IsEnabled = true;
            TamPizza =null;
        }

        private void checkMedia_Unchecked(object sender, RoutedEventArgs e)
        {
            qtdMaxPizza = 0;
            ExcluirSelecaoAposMudarTamanho();
            checkBroto.IsEnabled = true;
            checkGigante.IsEnabled = true;
            checkGrande.IsEnabled = true;
            TamPizza = null;
        }

        private void checkGrande_Unchecked(object sender, RoutedEventArgs e)
        {
            qtdMaxPizza = 0;
            ExcluirSelecaoAposMudarTamanho();
            checkBroto.IsEnabled = true;
            checkMedia.IsEnabled = true;
            checkGigante.IsEnabled = true;
            TamPizza = null;

        }

        private void checkGigante_Unchecked(object sender, RoutedEventArgs e)
        {
            qtdMaxPizza = 0;
            ExcluirSelecaoAposMudarTamanho();
            checkBroto.IsEnabled = true;
            checkMedia.IsEnabled = true;
            checkGrande.IsEnabled = true;
            TamPizza = null;
        }
    }
}
