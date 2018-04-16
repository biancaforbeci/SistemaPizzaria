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
    /// Interaction logic for EditarProdutos.xaml
    /// </summary>
    public partial class EditarProdutos : Window
    {
        private Pizza pizzaEditar = null;
        private Bebida bebidaEditar = null;
        private int referenciaProduto = 0;

        public EditarProdutos()
        {
            InitializeComponent();            
        }

        public void ProdutoEditarPizza(Pizza pizzaEdit,int refer)
        {
            txtPizza.Text = pizzaEdit.Nome;
            txtIngredientes.Text = pizzaEdit.Ingredientes;
            txtPrecoPizza.Text = Convert.ToString(pizzaEdit.Preco);
            pizzaEditar = pizzaEdit;
            referenciaProduto = refer;
            btnEditBebida.IsEnabled = false;
        }

        public void ProdutoEditarBebida(Bebida bebidaEdit, int refer)
        {
            txtBebida.Text = bebidaEdit.Nome;
            txtPreco.Text = Convert.ToString(bebidaEdit.Preco);
            referenciaProduto = refer;
            btnEditPizza.IsEnabled = false;
        }

        private Pizza EditarPizza()
        {
            Pizza pizza = new Pizza();
            pizza.Nome = txtPizza.Text;
            pizza.Ingredientes = txtIngredientes.Text;
            pizza.Preco = double.Parse(txtPrecoPizza.Text);
            return pizza;
        }

        private Bebida EditarBebida()
        {
            Bebida bebida = new Bebida();
            bebida.Nome = txtBebida.Text;
            bebida.Preco = double.Parse(txtPreco.Text);
            return bebida;
        }

        private void btnEditPizza_Click(object sender, RoutedEventArgs e)
        {
            PizzaController.EditarPizza(pizzaEditar.PizzaID,EditarPizza());
            MessageBox.Show("Editado com sucesso");
        }

        private void btnEditBebida_Click(object sender, RoutedEventArgs e)
        {
            BebibasController.EditarBebida(bebidaEditar.BebidaID, EditarBebida());
            MessageBox.Show("Editado com sucesso");
        }       

        private void btnVoltarCadastro_Click(object sender, RoutedEventArgs e)
        {
            if (referenciaProduto == 1)
            {
                CadastroPizzas tela = new CadastroPizzas();
                this.Close();
                tela.ShowDialog();
            }else if (referenciaProduto == 2)
            {
                Bebidas tela = new Bebidas();
                this.Close();
                tela.ShowDialog();
            }
        }

        private void btnVoltarMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow tela = new MainWindow();
            this.Close();
            tela.ShowDialog();
        }
    }
}
