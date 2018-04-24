﻿using Controllers;
using Models;
using System;
using System.Collections.Generic;
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
            txtBrotoEdit.Text = Convert.ToString(pizzaEdit.PrecoBroto);
            txtMediaEdit.Text = Convert.ToString(pizzaEdit.PrecoMedia);
            txtGrandeEdit.Text = Convert.ToString(pizzaEdit.PrecoGrande);
            txtGiganteEdit.Text = Convert.ToString(pizzaEdit.PrecoGigante);
            pizzaEditar = pizzaEdit;
            referenciaProduto = refer;
            btnEditBebida.IsEnabled = false;           
        }

        public void ProdutoEditarBebida(Bebida bebidaEdit, int refer)
        {
            txtBebida.Text = bebidaEdit.Nome;
            txtPreco.Text = Convert.ToString(bebidaEdit.Preco);
            bebidaEditar = bebidaEdit;
            referenciaProduto = refer;
            btnEditPizza.IsEnabled = false;            
        }
       

        private Pizza EditarPizza()
        {
            Pizza pizza = new Pizza();
            pizza.Nome = txtPizza.Text;
            pizza.Ingredientes = txtIngredientes.Text;
            pizza.PrecoBroto = Decimal.Parse(txtBrotoEdit.Text);
            pizza.PrecoMedia = Decimal.Parse(txtMediaEdit.Text);
            pizza.PrecoGrande = Decimal.Parse(txtGrandeEdit.Text);
            pizza.PrecoGigante = Decimal.Parse(txtGiganteEdit.Text);
            return pizza;
        }

        private Bebida EditarBebida()
        {
            Bebida bebida = new Bebida();
            bebida.Nome = txtBebida.Text;
            bebida.Preco = Decimal.Parse(txtPreco.Text);
            return bebida;
        }

        private void btnEditPizza_Click(object sender, RoutedEventArgs e)
        {
            string verifica = "^[0-9]";
            if (txtBrotoEdit.Text == "" || txtMediaEdit.Text == "" || txtGrandeEdit.Text == "" || txtGiganteEdit.Text == "" || txtPizza.Text == "")
            {
                MessageBox.Show("Erro, todos os preços precisam estar preenchidos !", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if ((!Regex.IsMatch(txtBrotoEdit.Text.Substring(0, 1), verifica)) || (!Regex.IsMatch(txtMediaEdit.Text.Substring(0, 1), verifica)) || (!Regex.IsMatch(txtGiganteEdit.Text.Substring(0, 1), verifica)) || (!Regex.IsMatch(txtGrandeEdit.Text.Substring(0, 1), verifica)))
            {
                MessageBox.Show("Digite, apenas números e valores monetários.");
            }
            else
            {
                PizzaController.EditarPizza(pizzaEditar.PizzaID, EditarPizza());
                MessageBox.Show("Editado com sucesso");
            }            
        }

        private void btnEditBebida_Click(object sender, RoutedEventArgs e)
        {
            string verifica = "^[0-9]";
            if ((!Regex.IsMatch(txtPreco.Text.Substring(0, 1), verifica)))
            {
                MessageBox.Show("Digite apenas valores númericos no campo preço.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                BebidasController.EditarBebida(bebidaEditar.BebidaID, EditarBebida());
                MessageBox.Show("Editado com sucesso");
            }
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
