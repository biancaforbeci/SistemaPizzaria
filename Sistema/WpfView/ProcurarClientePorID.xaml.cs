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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfView
{
    /// <summary>
    /// Interaction logic for ProcurarClientePorID.xaml
    /// </summary>
    public partial class ProcurarClientePorID : Window
    {
        public ProcurarClientePorID()
        {
            InitializeComponent();
            
        }

        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
             List<Cliente> dt = ClienteController.ListarTodosClientes();
             GridMostrar.ItemsSource = dt;
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
        }

        private void VerificaExistenciaCliente(List<Cliente> lista)
        {
          /*  if (lista.Contains(null))
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Telefone não cadastrado ! Deseja cadastrar cliente ?", "Cliente não encontrado", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes)
                {
                    CadastrarCliente ccli = new CadastrarCliente();
                    this.Close();
                    ccli.ShowDialog();
                }
            }
            else
            {
                GridMostrar.ItemsSource = lista;
            }   */         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CadastrarCliente cc = new CadastrarCliente();
            cc.ShowDialog();
        }
    }
}
