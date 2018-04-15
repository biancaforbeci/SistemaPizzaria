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
    /// Interaction logic for CadastroPizzas.xaml
    /// </summary>
    public partial class CadastroPizzas : Window
    {
        public CadastroPizzas()
        {
            InitializeComponent();
            LimparDados();
        }

        private void btnListarPizzas_Click(object sender, RoutedEventArgs e)
        {
            List<Pizza> list = BebidaController.ListarTodasPizzas();
            if (list!= null)
            {
                GridMostrarPizza.ItemsSource = list;
            }
            else
            {
                MessageBox.Show("A tabela não possui nada cadastrado");
            }
        }

        private void btnSalvarPizza_Click(object sender, RoutedEventArgs e)
        {
            Pizza pizza = SalvarPizza();
            BebidaController.SalvarPizza(pizza);
            LimparDados();
        }

        private Pizza SalvarPizza()
        {
            Pizza p = new Pizza();
            p.Nome = txtPizza.Text;
            p.Ingredientes = txtIngredientes.Text;
            p.Preco = double.Parse(txtPreco.Text);

            return p;
        }

        private void LimparDados()
        {
            btnEditar.IsEnabled = false;
            txtPizza.Text = "";
            txtIngredientes.Text = "";
            txtPreco.Text = "";

        }

        private void GridMostrarPizza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid Dg = (DataGrid)sender;
            DataRowView linha = Dg.SelectedItem as DataRowView;
            btnSalvarPizza.IsEnabled = false;

            if (linha != null)
            {
                txtPizza.Text = linha["Nome"].ToString();
                txtIngredientes.Text = linha["Ingredientes"].ToString();
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
