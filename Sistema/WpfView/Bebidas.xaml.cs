using Controllers;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Bebidas.xaml
    /// </summary>
    public partial class Bebidas : Window
    {
        public Bebidas()
        {
            InitializeComponent();           
        }

        private void btnListarBebidas_Click(object sender, RoutedEventArgs e)
        {
            List<Bebida> list = BebidasController.ListarTodasBebidas();
            if (list != null)
            {
                GridMostrarBebida.ItemsSource = list;
            }
            else
            {
                MessageBox.Show("A tabela não possui nada cadastrado");
            }
        }

        private void btnSalvarBebida_Click(object sender, RoutedEventArgs e)
        {
            Bebida bebida=SalvarBebida();
            BebidasController.SalvarBebidas(bebida);            
        }

        private Bebida SalvarBebida()
        {
            Bebida b = new Bebida();
            b.Nome = txtBebida.Text;
            b.Preco = double.Parse(txtPreco.Text);
            return b;
        }

        private void GridMostrarBebida_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridMostrarBebida.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Deseja editar a bebida " + ((Bebida)GridMostrarBebida.SelectedItem).Nome + " ?", "Edição", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        Bebida bebida = ((Bebida)GridMostrarBebida.SelectedItem);
                        EditarProdutos edit = new EditarProdutos();
                        edit.ProdutoEditarBebida(bebida,2);
                        this.Close();
                        edit.ShowDialog();
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("ERRO: " + erro);
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
        }
    }
}
