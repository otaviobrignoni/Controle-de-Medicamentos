using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.MedicationModule;

public interface IMedicationRepository : IRepository<Medication>
{
    bool HasMedicationForSupplier(Supplier? supplier);
}
