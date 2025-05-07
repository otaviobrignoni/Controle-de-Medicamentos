using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.InRequestsModule;
public interface IInRequestRepository : IRepository<InRequest>
{
    /// <summary>Verifica se há requisições de entrada associadas ao funcionário informado.</summary>
    /// <param name="employee">Funcionário a ser verificado.</param>
    /// <returns><c>true</c> se houver requisições vinculadas; caso contrário, <c>false</c>.</returns>
    bool HasRequisitionsForEmployee(Employee? employee);

    /// <summary>Verifica se há requisições de entrada associadas ao medicamento informado.</summary>
    /// <param name="medication">Medicamento a ser verificado.</param>
    /// <returns><c>true</c> se houver requisições vinculadas; caso contrário, <c>false</c>.</returns>
    bool HasRequisitionsForMedication(Medication? medication);
}
