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
            string[] options = new[]{"Cadastrar Fornecedor", "Editar Fornecedor", "Excluir Fornecedor", "Visualizar Fornecedor", "Voltar"};

            base.ShowMenu("Gerenciamento de Fornecedores", options, ExecuteOption);
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
