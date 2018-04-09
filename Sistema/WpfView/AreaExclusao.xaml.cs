using Controllers;
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
    /// Interaction logic for AreaExclusao.xaml
    /// </summary>
    public partial class AreaExclusao : Window
    {
        private int check = 0;

        public AreaExclusao()
        {
            InitializeComponent();
        }

        private void CheckPizza_Checked(object sender, RoutedEventArgs e)
        {
            CheckBebiba.IsEnabled = false;
            CheckCliente.IsEnabled = false;
            check = 1;
        }

        private void CheckBebiba_Checked(object sender, RoutedEventArgs e)
        {
            CheckCliente.IsEnabled = false;
            CheckPizza.IsEnabled = false;
            check = 2;
        }

        private void CheckCliente_Checked(object sender, RoutedEventArgs e)
        {
            CheckPizza.IsEnabled = false;
            CheckBebiba.IsEnabled = false;
            check = 3;
        }

        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            if (check==1)
            {
                PizzaController pc = new PizzaController();
                DataTable dt = pc.ExibirDados();
                GridMostrar.ItemsSource = dt.DefaultView;
            }
            else if(check==2)
            {
                BebibasController pc = new BebibasController();
                DataTable dt = pc.ExibirDados();
                GridMostrar.ItemsSource = dt.DefaultView;
            }
            else if (check == 3)
            {
                ClienteController pc = new ClienteController();
                DataTable dt = pc.ExibirDados();
                GridMostrar.ItemsSource = dt.DefaultView;
            }
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
          // GridMostrar.ro          
        }
    }
}
