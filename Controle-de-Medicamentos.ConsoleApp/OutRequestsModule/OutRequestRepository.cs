using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.OutRequestsModule;
public class OutRequestRepository : BaseRepository<OutRequest>, IOutRequestRepository
{

    public OutRequestRepository(DataContext context) : base(context)
    {
    }

    public override void Add(OutRequest entity)
    {
        foreach (PrescriptionMedication pm in entity.MedicalPrescription.Medications)
            pm.Medication.SubstractQuantity(pm.Quantity);
        base.Add(entity);
    }

    public override List<OutRequest> GetList()
    {
        return Context.OutRequests;
    }
}
