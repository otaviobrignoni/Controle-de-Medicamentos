using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.InRequestsModule;
public interface IInRequestRepository : IRepository<InRequest>
{
    bool HasRequisitionsForEmployee(Employee? employee);
    bool HasRequisitionsForMedication(Medication? medication);
}
