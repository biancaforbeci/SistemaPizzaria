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
    /// Interaction logic for ListaPedidos.xaml
    /// </summary>
    public partial class ListaPedidos : Window
    {
        public ListaPedidos()
        {
            InitializeComponent();
            MostrarGrid();
        }
        
        private void MostrarGrid()
        {
            List<Pedido> list=PedidoController.ListarTodosPedidos();
            if (list!=null)
            {
                gridPedidos.ItemsSource = list;
            }
            else
            {
                MessageBox.Show("Nenhum pedido cadastrado");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow tela = new MainWindow();
            this.Close();
            tela.ShowDialog();
        }
    }
}
