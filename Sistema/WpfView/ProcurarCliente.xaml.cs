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
    /// Interaction logic for ProcurarCliente.xaml
    /// </summary>
    public partial class ProcurarCliente : Window
    {
        public ProcurarCliente()
        {
            InitializeComponent();
        }

        public void ListaGrid()
        {

        }

        
        private void ProcuraID(object sender, RoutedEventArgs e)
        {
            ProcurarClientePorID pID = new ProcurarClientePorID();
            this.Close();
            pID.ShowDialog();
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
        }

        private void btnProcura_Click(object sender, RoutedEventArgs e)
        {
            ClienteController cc = new ClienteController();
            DataTable Dt = cc.ProcurarTelefone(txtTelefone.Text.Trim());

            if (Dt.Rows.Count==0)
            {
               MessageBoxResult result =MessageBox.Show("Telefone não cadastrado ! Deseja cadastrar cliente ?", "Cliente não encontrado", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result==MessageBoxResult.Yes)
                {
                    CadastrarCliente ccli = new CadastrarCliente();
                    this.Close();
                    ccli.ShowDialog();
                }
            }
            else
            {
                GridMostrar.ItemsSource = Dt.DefaultView;
            }
        }
    }
}
