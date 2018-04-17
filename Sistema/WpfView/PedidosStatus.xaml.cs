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
    /// Interaction logic for PedidosStatus.xaml
    /// </summary>
    public partial class PedidosStatus : Window
    {
        public PedidosStatus()
        {
            InitializeComponent();
        }

        private void gridPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridPedidos.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Deseja alterar para finalizado o status do item " + ((Pedido)gridPedidos.SelectedItem).PedidoID + " ?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        Cliente cli = ((Cliente)gridPedidos.SelectedItem);
                        //PedidoController.MudarStatus(cli);
                        MessageBox.Show("Pedido mudado para finalizado com sucesso");
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("ERRO: " + erro);
                    }
                }
            }
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow tela = new MainWindow();
            this.Close();
            tela.ShowDialog();
        }

        private void btnFinalizados_Click(object sender, RoutedEventArgs e)
        {
            List<Pedido> ped=PedidoController.ProcuraPedidoFinalizados();
            if (ped!=null)
            {
                gridPedidos.ItemsSource = ped;
            }
            else
            {
                MessageBox.Show("Nada encontrado nos pedidos finalizados");
            }
        }

        private void btnAndamento_Click(object sender, RoutedEventArgs e)
        {
            List<Pedido> ped = PedidoController.ProcuraPedidoPendentes();
            if (ped != null)
            {
                gridPedidos.ItemsSource = ped;
            }
            else
            {
                MessageBox.Show("Nada encontrado nos pedidos em andamento");
            }
        }
    }
}
