using Models;
using Sistema.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controllers
{
    public class EnderecoController
    {

        public static void SalvarEndereco(Endereco end)
        {
            ContextoSingleton.Instancia.TblEnderecos.Add(end);
            ContextoSingleton.Instancia.SaveChanges();
        }

        public static List<Endereco> ListarTodosEnderecos()
        {
            return ContextoSingleton.Instancia.TblEnderecos.ToList(); //IQueryable
        }       

        public static void EditarEndereco(int id, Endereco novoEnd)
        {

            Endereco end = ContextoSingleton.Instancia.TblEnderecos.Find(id);

            if (end != null)
            {
                end.Rua = novoEnd.Rua;
                end.Numero = novoEnd.Numero;
                end.Bairro = novoEnd.Bairro;
                end.Complemento = novoEnd.Complemento;
                end.Referencia = novoEnd.Referencia;
            }

            ContextoSingleton.Instancia.Entry(end).State =
                System.Data.Entity.EntityState.Modified;

            ContextoSingleton.Instancia.SaveChanges();
        }

        public static void ExcluirEndereco(int id)
        {

            Endereco endAtual = ContextoSingleton.Instancia.TblEnderecos.Find(id);

            ContextoSingleton.Instancia.Entry(endAtual).State =
                System.Data.Entity.EntityState.Deleted;
            ContextoSingleton.Instancia.SaveChanges();

        }  
               
    }
}