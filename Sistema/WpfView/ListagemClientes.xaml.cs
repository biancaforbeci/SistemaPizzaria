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
    /// Interaction logic for ListagemClientes.xaml
    /// </summary>
    public partial class ListagemClientes : Window
    {
        public ListagemClientes()
        {
            InitializeComponent();
            MostrarClientes();
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
        }        

            private void MostrarClientes()
            {
                List<Cliente> dt = ClienteController.ListarTodosClientes();
                if (dt != null)
                {
                    gridCliente.ItemsSource = dt;
                }

            }

        }
    }
