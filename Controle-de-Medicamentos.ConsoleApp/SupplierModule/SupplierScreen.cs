using System.ComponentModel;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Utils;

namespace Controle_de_Medicamentos.ConsoleApp.SupplierModule
{
    public class SupplierScreen : BaseScreen<Supplier>, ICrudScreen
    {
        public SupplierScreen(ISupplierRepository repository) : base(repository, "Fornecedor") { }


        public override void ShowMenu()
        {
            string[] opcoes = new[]{"Cadastrar Fornecedor", "Editar Fornecedor", "Excluir Fornecedor", "Visualizar Fornecedor", "Voltar"};

            int indiceSelecionado = 0;
            ConsoleKey tecla;

            do
            {
                Console.Clear();
                Write.Header("Gerenciamento de Fornecedores");
                Console.WriteLine();

                for (int i = 0; i < opcoes.Length; i++)
                {
                    if (i == indiceSelecionado)
                        Write.InColor($"-> {opcoes[i]}", ConsoleColor.Green);
                    else
                        Console.WriteLine($"   {opcoes[i]}");
                }

                tecla = Console.ReadKey(true).Key;

                switch (tecla)
                {
                    case ConsoleKey.UpArrow:indiceSelecionado = (indiceSelecionado == 0) ? opcoes.Length - 1 : indiceSelecionado - 1; break;

                    case ConsoleKey.DownArrow: indiceSelecionado = (indiceSelecionado + 1) % opcoes.Length; break;

                    case ConsoleKey.Enter:ExecutarOpcao(indiceSelecionado);
                        if (indiceSelecionado == 4) return; break;

                    case ConsoleKey.Escape: return;
                }
            } while (true);
        }

        private void ExecutarOpcao(int indice)
        {
            switch (indice)
            {
                case 0: Add(); break;
                case 1: Edit(); break;
                case 2: Remove(); break;
                case 3: ShowAll(true, true); break;
                case 4: break;
                default: Write.ShowInvalidOption(); break;
            }
        }

        protected override Supplier NewEntity()
        {
            Write.InColor("> Digite o nome do fornecedor: ", ConsoleColor.Yellow, true);
            string name = Console.ReadLine()!.Trim().ToTitleCase();

            Write.InColor("> Digite o telefone do fornecedor: ", ConsoleColor.Yellow, true);
            string phone = Console.ReadLine()!.Trim().ToTitleCase();

            Write.InColor("> Digite o CNPJ do fornecedor: ", ConsoleColor.Yellow, true);
            string cnpj = Console.ReadLine()!.Trim().ToTitleCase();

            return new Supplier(name, phone, cnpj);
        }

        public override string[] GetHeaders()
        {
            return new[] { "Id", "Nome", "Telefone", "CNPJ" };
        }
    }
}
