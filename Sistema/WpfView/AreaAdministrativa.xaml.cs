using Controllers;
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
    /// Interaction logic for AreaAdministrativa.xaml
    /// </summary>
    public partial class AreaAdministrativa : Window
    {
        public AreaAdministrativa()
        {
            InitializeComponent();
        }

        private void btnEnviarLogin_Click(object sender, RoutedEventArgs e)
        {
            AdministradorController ac = new AdministradorController();

            bool resp = ac.Permissao(txtLogin.Text, txtSenha.Text);

            if (resp == true)
            {
                AreadeExclusao ae = new AreadeExclusao();
                ae.ShowDialog();
            }
            else
            {
                MessageBox.Show("Erro !", "Erro de login", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
    }
}
