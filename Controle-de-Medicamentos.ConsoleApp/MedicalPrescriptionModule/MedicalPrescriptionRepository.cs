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

    public override List<MedicalPrescription> GetAll() 
    { 
        foreach (MedicalPrescription item in List)
        {
           item.SetExpired();
        }
        Context.SaveData();
        return List;
    }
}
