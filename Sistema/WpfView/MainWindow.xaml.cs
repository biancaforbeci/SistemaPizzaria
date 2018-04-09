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
                ProcurarCliente  cl = new ProcurarCliente();
                cl.ShowDialog();
            
            // *****************************//
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            CadastrarCliente cc = new CadastrarCliente();
            this.Close();
            cc.ShowDialog();
        }

        private void ItemPizzas_Click(object sender, RoutedEventArgs e)
        {
            CadastroPizzas cp = new CadastroPizzas();
            this.Close();
            cp.ShowDialog();
        }

        private void ItemBebibas_Click(object sender, RoutedEventArgs e)
        {
            Bebidas b = new Bebidas();
            this.Close();
            b.ShowDialog();
        }

        private void Exclusao_Click(object sender, RoutedEventArgs e)
        {
            AreaAdministrativa ad = new AreaAdministrativa();
            this.Close();
            ad.ShowDialog();
        }
    }
}
