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
            LimparDados();
        }

        private void btnListarBebidas_Click(object sender, RoutedEventArgs e)
        {
            List<Bebida> list = BebibasController.ListarTodasBebidas();
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
            BebibasController.SalvarBebidas(bebida);
            LimparDados();
        }

        private Bebida SalvarBebida()
        {
            Bebida b = new Bebida();
            b.Nome = txtBebida.Text;
            b.Preco = double.Parse(txtPreco.Text);
            return b;
        }

        private void LimparDados()
        {
            btnEditar.IsEnabled = false;
            txtBebida.Text = "";
            txtPreco.Text = "";

        }

        private void GridMostrarBebida_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid Dg = (DataGrid)sender;
            DataRowView linha = Dg.SelectedItem as DataRowView;
            btnSalvarBebida.IsEnabled = false;

            if (linha != null)
            {
                txtBebida.Text = linha["Nome"].ToString();
                txtPreco.Text = linha["Preco"].ToString();
                btnEditar.IsEnabled = true;
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
