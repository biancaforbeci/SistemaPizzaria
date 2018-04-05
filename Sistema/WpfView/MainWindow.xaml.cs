using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRealizarPedido_Click(object sender, RoutedEventArgs e)
        {         
                ProcurarCliente cl = new ProcurarCliente();
                cl.ShowDialog();
            
            // *****************************//
        }

        private void btnAreaADM_Click(object sender, RoutedEventArgs e)
        {
            AreaAdministrativa aa = new AreaAdministrativa();
            aa.ShowDialog();
        }
    }
}
