using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Utils;

namespace Controle_de_Medicamentos.ConsoleApp.SupplierModule
{
    public class SupplierScreen : BaseScreen<Supplier>, ICrudScreen
    {
        public ISupplierRepository SupplierRepository;

        public SupplierScreen(ISupplierRepository repository) : base(repository, "Fornecedor")
        {
            SupplierRepository = repository;
        }

        public override void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Write.Header("Gerenciamento de Fornecedores");
                Console.WriteLine();
                Write.WriteInColor(" [1] - Cadastrar Fornecedor", ConsoleColor.Cyan);
                Write.WriteInColor(" [2] - Editar Fornecedor", ConsoleColor.Cyan);
                Write.WriteInColor(" [3] - Excluir Fornecedor", ConsoleColor.Cyan);
                Write.WriteInColor(" [4] - Visualizar Fornecedor", ConsoleColor.Cyan);
                Write.WriteInColor(" [5] - Sair", ConsoleColor.Cyan);
                Console.WriteLine();
                Write.WriteInColor(">> Digite a opção desejada: ", ConsoleColor.Yellow, true);
                string option = Console.ReadLine()!;

                switch (option)
                {
                    case "1": Add(); break;
                    case "2": Edit(); break;
                    case "3": Remove(); break;
                    case "4": ShowAll(true, true); break;
                    case "5": return;
                    default: Write.ShowInvalidOptionMessage(); break;
                }
            }
        }

        protected override Supplier NewEntity()
        {
            Write.WriteInColor("> Digite o nome do fornecedor: ", ConsoleColor.Yellow, true);
            string name = Console.ReadLine()!.Trim().ToTitleCase();

            Write.WriteInColor("> Digite o telefone do fornecedor: ", ConsoleColor.Yellow, true);
            string phone = Console.ReadLine()!.Trim().ToTitleCase();

            Write.WriteInColor("> Digite o CNPJ do fornecedor: ", ConsoleColor.Yellow, true);
            string cnpj = Console.ReadLine()!.Trim().ToTitleCase();

            return new Supplier(name, phone, cnpj);
        }

        protected override void ShowTableHeader()
        {
            Console.WriteLine("");
        }

        protected override void ShowTableRow(Supplier entity)
        {
            Console.WriteLine("");
        }
    }
}
