using Controllers;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        
        public AreaExclusao()
        {
            InitializeComponent();
            PadraoComponentes();
        }

        private void CheckPizza_Checked(object sender, RoutedEventArgs e)
        {
            CheckBebiba.IsEnabled = false;
            CheckCliente.IsEnabled = false;
            txtPizza.IsEnabled = true;
            btnPesquisaPizza.IsEnabled = true;
        }

        private void CheckBebiba_Checked(object sender, RoutedEventArgs e)
        {
            CheckCliente.IsEnabled = false;
            CheckPizza.IsEnabled = false;
            txtBebida.IsEnabled = true;
            btnProcuraBebida.IsEnabled = true;
        }

        private void CheckCliente_Checked(object sender, RoutedEventArgs e)
        {
            CheckPizza.IsEnabled = false;
            CheckBebiba.IsEnabled = false;
            txtCliente.IsEnabled = true;
            btnPesquisaCliente.IsEnabled = true;
        }        

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
        }
        
        private void gridBebida_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridBebida.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Confirma a exclusão do item " + ((Bebida)gridBebida.SelectedItem).Nome + " ?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {      //Se confirmado a exclusão é pego o ID da linha selecionada.
                        int id = ((Bebida)gridBebida.SelectedItem).BebidaID;
                        BebibasController.ExcluirBebida(id);
                        MessageBox.Show("Bebida excluída com sucesso");
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("ERRO: " + erro);
                    }
                }
            }
        }

           private void gridPizza_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (gridPizza.SelectedItem != null)
                {
                    MessageBoxResult result = MessageBox.Show("Confirma a exclusão do item " + ((Pizza)gridBebida.SelectedItem).Nome + " ?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {      //Se confirmado a exclusão é pego o ID da linha selecionada.
                            int id = ((Pizza)gridPizza.SelectedItem).PizzaID;
                            PizzaController.ExcluirPizza(id);
                            MessageBox.Show("Pizza excluída com sucesso");
                        }
                        catch (Exception erro)
                        {
                            MessageBox.Show("ERRO: " + erro);
                        }
                    }
                }
            }

            private void gridCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (gridCliente.SelectedItem != null)
                {
                    MessageBoxResult result = MessageBox.Show("Confirma a exclusão do item " + ((Cliente)gridBebida.SelectedItem).Nome + " ?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {      //Se confirmado a exclusão é pego o ID da linha selecionada.
                            int id = ((Cliente)gridCliente.SelectedItem).ClienteID;
                            ClienteController.ExcluirCliente(id);
                            MessageBox.Show("Cliente excluído com sucesso");
                        }
                        catch (Exception erro)
                        {
                            MessageBox.Show("ERRO: " + erro);
                        }
                    }
                }
            }
        
        private void MensagemErro()
        {
            MessageBox.Show("Erro, campo digitado não foi encontrado.");
        }

        private void btnProcuraBebida_Click(object sender, RoutedEventArgs e)
        {
            if ((txtBebida.Text != null) || (Regex.IsMatch(txtBebida.Text, @"^[a-zA-Z]+$")) )
            {
                List<Bebida> bebida = BebibasController.PesquisarPorNome(txtBebida.Text);
                if (bebida != null)
                {
                    gridBebida.ItemsSource = bebida;
                }
                else
                {
                    MensagemErro();
                }
            }
            else
            {
                MessageBox.Show("Erro no campo digitado !");
            }
        }
  
        private void btnPesquisaPizza_Click(object sender, RoutedEventArgs e)
        {
            if(txtPizza.Text != null || (Regex.IsMatch(txtBebida.Text, @"^[a-zA-Z]+$") ))
            {
              List<Pizza> pizza= PizzaController.PesquisarPorNome(txtPizza.Text);
                if (pizza != null)
                {  
                    gridPizza.ItemsSource = pizza;
                }
                else
                {
                    MensagemErro();
                }
            }
            else
            {
                MessageBox.Show("Erro no campo digitado !");
            }
        }

        private void btnPesquisaCliente_Click(object sender, RoutedEventArgs e)
        {
            if (txtCliente.Text != null || (Regex.IsMatch(txtBebida.Text, @"^[a-zA-Z]+$") == true))
            {
                List<Cliente>cli = ClienteController.PesquisarPorNome(txtCliente.Text);
                if (cli != null)
                {
                    gridCliente.ItemsSource = cli;
                }
                else
                {
                    MensagemErro();
                }
            }
            else
            {
                MessageBox.Show("Erro no campo digitado !");
            }
        }

        private void CheckBebiba_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckPizza.IsEnabled = true;
            CheckCliente.IsEnabled = true;
           PadraoComponentes();
        }

        private void CheckPizza_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBebiba.IsEnabled = true;
            CheckCliente.IsEnabled = true;
            PadraoComponentes();
        }

        private void CheckCliente_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckPizza.IsEnabled = true;
            CheckBebiba.IsEnabled = true;
            PadraoComponentes();
        }

        private void PadraoComponentes()
        {
            txtPizza.IsEnabled = false;
            txtCliente.IsEnabled = false;
            txtBebida.IsEnabled = false;
            btnPesquisaPizza.IsEnabled = false;
            btnProcuraBebida.IsEnabled = false;
            btnPesquisaCliente.IsEnabled = false;
        }
    }
}

