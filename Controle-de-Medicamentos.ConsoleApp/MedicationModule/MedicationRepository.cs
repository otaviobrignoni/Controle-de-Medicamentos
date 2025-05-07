using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.MedicationModule;

public class MedicationRepository : BaseRepository<Medication>, IMedicationRepository
{
    public MedicationRepository(DataContext context) : base(context) {}

    public override List<Medication> GetList()
    {
       return Context.Medications;
    }

    public override void Add(Medication entity)
    {
        if (TryMergeWithExisting(entity))
        {
            Context.SaveData();
            return;
        }
        entity.Id = GetNextAvailableId();
        List.Add(entity);
        Context.SaveData();
    }

    /// <summary>
    /// Verifica se já existe um medicamento equivalente e, se existir, atualiza sua quantidade.
    /// </summary>
    /// <param name="newMedication">Novo medicamento a ser verificado.</param>
    /// <returns><c>true</c> se houve consolidação com um medicamento existente; caso contrário, <c>false</c>.</returns>
    public bool TryMergeWithExisting(Medication newMedication)
    {
        Medication existing = GetAll().FirstOrDefault(m => m.IsSameMedication(newMedication));
        if (existing != null)
        {
            existing.UpdateQuantity(newMedication.Quantity);
            return true;
        }
        return false;
    }

    public bool HasMedicationForSupplier(Supplier supplier)
    {
        return List.Any(m => m.Supplier.Id == supplier.Id);
    }
}
