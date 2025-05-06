using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Utils;

namespace Controle_de_Medicamentos.ConsoleApp.PatientModule
{
    public class PatientScreen : BaseScreen<Patient>, ICrudScreen
    {
        public PatientScreen(IPatientRepository repository) : base(repository, "Paciente") {}

        public override void ShowMenu()
        {
            string[] options = new[] { "Cadastrar Paciente", "Editar Paciente", "Excluir Paciente", "Visualizar Pacientes", "Voltar" };

            base.ShowMenu("Gerenciamento de Pacientes", options, ExecuteOption);
        }

        protected override Patient NewEntity()
        {
            Write.InColor("> Digite o nome do paciente: ", ConsoleColor.Yellow, true);
            string name = Console.ReadLine()!.Trim().ToTitleCase();

            Write.InColor("> Digite o telefone do paciente: ", ConsoleColor.Yellow, true);
            string phone = Console.ReadLine()!.Trim().ToTitleCase();

            Write.InColor("> Digite o cartão do SUS do paciente: ", ConsoleColor.Yellow, true);
            string susCard = Console.ReadLine()!.Trim().ToTitleCase();

            return new Patient(name, phone, susCard);
        }

        public override string[] GetHeaders()
        {
            return new[] { "Id", "Nome", "Telefone", "Cartão SUS" };
        }
    }
}
