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
            ClienteController cc = new ClienteController();
            DataTable dt = cc.ExibirDados();
            GridMostrar.ItemsSource = dt.DefaultView;
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
        }
    }
}
