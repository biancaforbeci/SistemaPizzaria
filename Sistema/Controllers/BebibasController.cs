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
    public class BebibasController
    {
        // INSERT
        public static void SalvarBebidas(Bebida bebida )
        {
            ContextoSingleton.Instancia.TblBebida.Add(bebida);
            ContextoSingleton.Instancia.SaveChanges();
        }

        public static List<Bebida> ListarTodasBebidas()
        {

            return ContextoSingleton.Instancia.TblBebida.ToList(); //IQueryable
        }

        public static void EditarBebida(int id, Bebida novaBebida)
        {

            Bebida bebidaEdit = PesquisarPorID(id);

            if (bebidaEdit != null)
            {
                bebidaEdit.Nome = novaBebida.Nome;
                bebidaEdit.Preco = novaBebida.Preco;
            }

            ContextoSingleton.Instancia.Entry(bebidaEdit).State =
                System.Data.Entity.EntityState.Modified;

            ContextoSingleton.Instancia.SaveChanges();
        }

        public static void ExcluirBebida(int id)
        {

            Bebida bebidaAtual = ContextoSingleton.Instancia.TblBebida.Find(id);

            ContextoSingleton.Instancia.Entry(bebidaAtual).State =
                System.Data.Entity.EntityState.Deleted;
            ContextoSingleton.Instancia.SaveChanges();

        }

        public static Bebida PesquisarPorID(int IDBebida)
        {
            return ContextoSingleton.Instancia.TblBebida.Find(IDBebida);
        }

        public static List<Bebida> PesquisarPorNome(string nome)
        {
            var c = (from x in ContextoSingleton.Instancia.TblBebida
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

