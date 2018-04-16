using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controllers
{
    public class PizzaController
    {

        // INSERT
        public static void SalvarPizza(Pizza pizza)
        {
            ContextoSingleton.Instancia.TblPizza.Add(pizza);
            ContextoSingleton.Instancia.SaveChanges();
        }

        public static List<Pizza> ListarTodasPizzas()
        {
             List<Pizza> list=ContextoSingleton.Instancia.TblPizza.ToList(); //IQueryable

            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        public static void EditarPizza(int id, Pizza novaPizza)
        {

            Pizza pizzaEdit = PesquisarPorID(id);

            if (pizzaEdit != null)
            {
                pizzaEdit.Nome = novaPizza.Nome;
                pizzaEdit.Ingredientes = novaPizza.Ingredientes;
                pizzaEdit.Preco = novaPizza.Preco;
            }

            ContextoSingleton.Instancia.Entry(pizzaEdit).State =
                System.Data.Entity.EntityState.Modified;

            ContextoSingleton.Instancia.SaveChanges();
        }

        public static void ExcluirPizza(int id)
        {

            Pizza pizzaAtual = ContextoSingleton.Instancia.TblPizza.Find(id);

            ContextoSingleton.Instancia.Entry(pizzaAtual).State =
                System.Data.Entity.EntityState.Deleted;
            ContextoSingleton.Instancia.SaveChanges();

        }

        public static Pizza PesquisarPorID(int IDPizza)
        {
            return ContextoSingleton.Instancia.TblPizza.Find(IDPizza);
        }

        public static List<Pizza> PesquisarPorNome(string nome)
        {

            var c = (from x in ContextoSingleton.Instancia.TblPizza
                    where x.Nome.ToLower().Equals(nome.ToLower())
                    select x).ToList();

            if (c.Count > 0)
            {
                return c;
            }
            else
            {
                return null;
            }
        }
    }
}