using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.InRequestsModule;
public class InRequestRepository : BaseRepository<InRequest>, IInRequestRepository
{
    public InRequestRepository(DataContext context) : base(context){}

    public override void Add(InRequest inRequest)
    {
        inRequest.Medication.UpdateQuantity(inRequest.Quantity);
        base.Add(inRequest);
    }

    public override List<InRequest> GetList()
    {
        return Context.InRequests;
    }
  
    public bool HasRequisitionsForMedication(Medication medication)
    {
        return List.Any(i => i.Medication.Id == medication.Id);
    }

    public bool HasRequisitionsForEmployee(Employee employee)
    {
        return List.Any(i => i.Employee.Id == employee.Id);
    }
}
