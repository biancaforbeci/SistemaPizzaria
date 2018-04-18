﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
   public class ClientesPizzasController
    {
        public static void SalvarItem(ClientesPizzas novo)
        {
            ContextoSingleton.Instancia.TblClientesPizzas.Add(novo);
            ContextoSingleton.Instancia.SaveChanges();
        }

        public static List<ClientesPizzas> PesquisarClientePedidos(int cliID)
        {
            var c = (from x in ContextoSingleton.Instancia.TblClientesPizzas.Include("_Cliente")
                     where x.ClienteID.Equals(cliID)
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

        public static void ExcluirSelecao(int id)
        {
            ClientesPizzas pizzaExcluir = ContextoSingleton.Instancia.TblClientesPizzas.Find(id);

            ContextoSingleton.Instancia.Entry(pizzaExcluir).State =
                System.Data.Entity.EntityState.Deleted;
            ContextoSingleton.Instancia.SaveChanges();
        }

        public static void ExcluirPedidosCliente(int cliID)
        {
            var c = (from x in ContextoSingleton.Instancia.TblClientesPizzas.Include("_Cliente")
                    where x.ClienteID.Equals(cliID)
                    select x).ToList();
            foreach (var item in c)
            {
                ContextoSingleton.Instancia.Entry(item).State =
               System.Data.Entity.EntityState.Deleted;
                ContextoSingleton.Instancia.SaveChanges();
            }            
        }
        
    }
}
