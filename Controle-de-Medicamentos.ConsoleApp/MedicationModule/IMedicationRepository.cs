using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.MedicationModule;

public interface IMedicationRepository : IRepository<Medication>
{
    /// <summary>
    /// Verifica se existe ao menos um medicamento associado ao fornecedor informado.
    /// </summary>
    /// <param name="supplier">Fornecedor a ser verificado.</param>
    /// <returns>
    /// <c>true</c> se houver medicamentos vinculados ao fornecedor; caso contrário, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Este método é útil para validar restrições de exclusão de fornecedores com medicamentos associados.
    /// </remarks>
    bool HasMedicationForSupplier(Supplier? supplier);
}
