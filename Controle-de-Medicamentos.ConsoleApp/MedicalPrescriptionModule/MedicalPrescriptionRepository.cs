using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionsModule;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;

public class MedicalPrescriptionRepository : BaseRepository<MedicalPrescription>, IMedicalPrescriptionRepository
{
    public MedicalPrescriptionRepository(DataContext context) : base(context) {}

    public override List<MedicalPrescription> GetList()
    {
        return Context.MedicalPrescriptions;
    }
}
