using Controllers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema
{
    class Program
    {
        enum OpcoesMenuPrincipal 
        {
            CadastroCliente = 1,
            EditarCliente = 2,
            ExcluirCliente = 3,
            RealizarPedido = 4,
            EntrarAreaADM  = 5,
            Sair = 6
        }

        private static  OpcoesMenuPrincipal MenuPrincipal()
        {
            Console.WriteLine("---Bem-Vindo a Engenheiros da Pizza ---");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("-------Menu Inicial-------------");
            Console.WriteLine("1- Cadastro de novo cliente na pizzaria.");  // Essa parte estará na parte de realização do pedido, caso não encontre cliente cadastrado.
            Console.WriteLine("2- Editar cliente"); //Essa parte estará na parte de editar na realização do pedido, caso deseje arrumar algo depois que foi encontrado o cliente.
            Console.WriteLine("3- Excluir cliente");//Essa parte estará somente na área administrativa, caso queira excluir definitivamente um cliente.
            Console.WriteLine("4- Realizar Pedido"); 
            Console.WriteLine("5- Área Administrativa");
            Console.WriteLine("6- Sair");

            return (OpcoesMenuPrincipal)int.Parse(Console.ReadLine());
        }


        static void Main(string[] args)
        {

            OpcoesMenuPrincipal opcDigitada = OpcoesMenuPrincipal.Sair;
            do
            {
                opcDigitada = MenuPrincipal();

                switch (opcDigitada)
                {
                    case OpcoesMenuPrincipal.CadastroCliente:
                        Cliente novo = CadastroCliente();
                        InformacoesCliente(novo);                        
                        ConfirmaNovoCliente(novo);
                        break;
                    case OpcoesMenuPrincipal.EditarCliente:
                        EditarCliente();
                        break;
                    case OpcoesMenuPrincipal.ExcluirCliente:
                        ExcluirCliente();
                        break;
                    case OpcoesMenuPrincipal.RealizarPedido:
                        FazerPedido();
                        break;
                    case OpcoesMenuPrincipal.EntrarAreaADM:
                        AreaADM();
                        break;
                    case OpcoesMenuPrincipal.Sair:

                        break;
                    default:
                        Console.WriteLine("Erro,opção digitada incorreta");
                        break;
                }
              
            } while (opcDigitada != OpcoesMenuPrincipal.Sair);


        }

        private static Cliente CadastroCliente()
        {
            ClienteController cc = new ClienteController();
            Cliente c = new Cliente();
            Console.WriteLine();
            Console.WriteLine("-----------Área de Cadastro de Clientes------------------");
            Console.WriteLine("Digite o nome do cliente:");
            c.Nome = Console.ReadLine();

            Console.WriteLine("Digite o CPF:");
            c.Cpf = Console.ReadLine();

            Console.WriteLine("Digite o telefone de contato");
            c.Telefone = Console.ReadLine();
            Endereco end = CadastrarEndereco();
            c.EnderecoID =end.EnderecoID;

            cc.SalvarCliente(c);
            return c;
        }

        private static Endereco CadastrarEndereco()
        {
            EnderecoController ec = new EnderecoController();
            Endereco e = new Endereco();

            Console.WriteLine("Digite o nome da rua: ");
            e.Rua = Console.ReadLine();

            Console.WriteLine("Digite o número da rua: ");
            e.Numero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o bairro: ");
            e.Bairro = Console.ReadLine();

            Console.WriteLine("Digite o complemento: ");
            e.Complemento = Console.ReadLine();

            Console.WriteLine("Digite uma referência: ");
            e.Referencia = Console.ReadLine();

            ec.SalvarEndereco(e);

            return e;
        }

        private static void InformacoesCliente(Cliente c)
        {
            Console.WriteLine();
            Console.WriteLine("Nome: " + c.Nome);
            Console.WriteLine("CPF: " + c.Cpf);
            Console.WriteLine("Telefone: " + c.Telefone);
            
            ListarEndereco(c.EnderecoID);
        }

        private static void ListarEndereco(int id)
        {
            EnderecoController ec = new EnderecoController();

            Endereco e = ec.PesquisarPorIDEndereco(id);

            Console.WriteLine("Rua: " + e.Rua);
            Console.WriteLine("Número: " + e.Numero);
            Console.WriteLine("Bairro: " + e.Bairro);
            Console.WriteLine("Complemento: " + e.Complemento);
            Console.WriteLine("Referência: " + e.Referencia);
            Console.Write("-----------------------------------------------");

        }

        private static void ConfirmaNovoCliente(Cliente c)
        {
            int resp;
            do {
                Console.WriteLine();
            Console.WriteLine("Confirma dados do cliente ? Digite 1 se sim, 2 para editar cliente.");
             resp = int.Parse(Console.ReadLine());
            
                switch (resp)
                {
                    case 1:
                        return;
                    case 2:
                        Cliente CliEditado = ArrumarCadastro(c.PessoaID);

                        if (CliEditado != null)
                        {
                            Console.WriteLine();
                            InformacoesCliente(CliEditado);
                            Console.WriteLine("Cliente editado com sucesso");
                        }
                        ConfirmaNovoCliente(CliEditado);
                        return;
                    default:
                        Console.WriteLine("Erro, opção digitada incorreta.");
                        return;
                }

            } while (resp != 1  && resp != 2);
        }

        private static void ListarTodosClientes()
        {
            ClienteController cc = new ClienteController();

            foreach (var item in cc.MostrarLista())
            {

                Console.WriteLine("==========================");

                Console.WriteLine("ID: " + item.PessoaID);
                InformacoesCliente(item);

                Console.WriteLine("==========================");
            }
        }

        private static Cliente ArrumarCadastro(int IDEditar)
        {
            ClienteController cc = new ClienteController();
            Cliente CliEditar = cc.PesquisarPorIDCliente(IDEditar);

            if (CliEditar != null)
            {
                Console.WriteLine();
                Console.WriteLine("Nome novo: ");
                CliEditar.Nome = Console.ReadLine();
                Console.WriteLine("Telefone novo: ");
                CliEditar.Telefone= Console.ReadLine();
                Console.WriteLine("CPF novo: ");
                CliEditar.Cpf = Console.ReadLine();

                EditarEndereco(CliEditar.EnderecoID);

                cc.EditarCliente(IDEditar, CliEditar);

                return CliEditar;
            }
            Console.WriteLine("Erro, ID do cliente não encontrado");
            return null;


        }

        private static Cliente EditarCliente()
        {
            ListarTodosClientes();

            Console.WriteLine("Digite qual ID do cliente deseja alterar:");
            int IDEditar = int.Parse(Console.ReadLine());

            ClienteController cc = new ClienteController();
            Cliente CliEditar = cc.PesquisarPorIDCliente(IDEditar);

            if (CliEditar != null)
            {
                Console.WriteLine();
                Console.WriteLine("Nome: ");
                CliEditar.Nome = Console.ReadLine();
                Console.WriteLine("Telefone: ");
                CliEditar.Telefone= Console.ReadLine();
                Console.WriteLine("CPF: ");
                CliEditar.Cpf = Console.ReadLine();

                EditarEndereco(CliEditar.EnderecoID);                

                cc.EditarCliente(IDEditar, CliEditar);

                return CliEditar;
            }
            Console.WriteLine("Erro, ID do cliente não encontrado");
            return null;

        }

        private static void EditarEndereco(int id)
        {
            EnderecoController ec = new EnderecoController();
            Endereco EndEditar = ec.PesquisarPorIDEndereco(id);

            Console.WriteLine("Rua: ");
            EndEditar.Rua = Console.ReadLine();
            Console.WriteLine("Número: ");
            EndEditar.Numero = int.Parse(Console.ReadLine());
            Console.WriteLine("Bairro: ");
            EndEditar.Bairro = Console.ReadLine();
            Console.WriteLine("Complemento: ");
            EndEditar.Complemento = Console.ReadLine();
            Console.WriteLine("Referência: ");
            EndEditar.Referencia = Console.ReadLine();

            ec.EditarEndereco(id, EndEditar);

        }

        private static void ExcluirCliente()
        {
            ListarTodosClientes();

            Console.WriteLine("Digite qual ID deseja excluir: ");
            int IDExcluir = int.Parse(Console.ReadLine());

            ClienteController cc = new ClienteController();
            Cliente cliExcluir = cc.PesquisarPorIDCliente(IDExcluir);

            EnderecoController ec = new EnderecoController();

            ec.ExcluirEndereco(cliExcluir.EnderecoID);
            cc.ExcluirCLiente(cliExcluir.PessoaID);
        }

        private static void FazerPedido()
        {
            Console.WriteLine("Realizar busca por ID ? Digite 1 para sim, 2 para busca por telefone");
            int i = int.Parse(Console.ReadLine());

            if (i==1)
            {
                PesquisaCliente();
            }
                PesquisaPorTelefone();
        }

        private static void PesquisaCliente()
        {
            ClienteController cc = new ClienteController();
            ListarTodosClientes();

            Console.WriteLine();
            Console.WriteLine("Digite o ID de busca: ");
            int id = int.Parse(Console.ReadLine());
            Cliente c = cc.PesquisarPorIDCliente(id);

            InformacoesCliente(c);

            Console.WriteLine("Deseja alterar algo ? Digite 1 para sim e 2 para não");
            int alter = int.Parse(Console.ReadLine());

            if (alter.Equals(1))
            {
                ArrumarCadastro(c.PessoaID);
            }
            Console.WriteLine("=====================Área de Pedido===================");

        }

        private static void PesquisaPorTelefone()
        {

        }


        private static void AreaADM()
        {
            Console.WriteLine("===Área Administrativa==");
        }

    }
}
