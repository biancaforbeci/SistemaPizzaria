using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class EnderecoController
    {
        static private List<Endereco> EnderecosCadastrados = new List<Endereco>();
        int UltimoID = 0;

        public void SalvarEndereco(Endereco e)
        {
            int id = UltimoID + 1;
            UltimoID = id;
            e.EnderecoID = id;
            EnderecosCadastrados.Add(e);
        }

        public Endereco PesquisarPorIDEndereco(int id)
        {
            var a = from x in EnderecosCadastrados
                    where x.EnderecoID.Equals(id)
                    select x;

            if (a != null)
            {
                return a.FirstOrDefault();
            }
            return null;
        }

        public void EditarEndereco(int id, Endereco EndAtual)
        {
            Endereco EndAntigo = PesquisarPorIDEndereco(id);

            EndAntigo.Rua = EndAtual.Rua;
            EndAntigo.Numero = EndAtual.Numero;
            EndAntigo.Bairro = EndAtual.Bairro;
            EndAntigo.Complemento = EndAtual.Complemento;
            EndAntigo.Referencia = EndAtual.Referencia;
        }

        public void ExcluirEndereco(int id)
        {
            Endereco EndExcluir = PesquisarPorIDEndereco(id);

            if (EndExcluir != null)
            {
                EnderecosCadastrados.Remove(EndExcluir);
            }
               
        }

    }
}
