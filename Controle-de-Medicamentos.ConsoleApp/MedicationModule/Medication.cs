using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
namespace Controle_de_Medicamentos.ConsoleApp.MedicationModule;

public class Medication : BaseEntity<Medication>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public Supplier Supplier { get; set; }

    public Medication() { }

    public Medication(string name, string description, int quantity, Supplier supplier)
    {
        Name = name;
        Description = description;
        Quantity = quantity;
        Supplier = supplier;
    }
    public override void UpdateEntity(Medication entity)
    {
        Name = entity.Name;
        Description = entity.Description;
        Quantity = entity.Quantity;
        Supplier = entity.Supplier;
    }
    public override string Validate()
    {
        string erros = "";

        if (string.IsNullOrEmpty(Name))
            erros += "O Campo 'Nome' é obrigatório\n";

        if (Name.Length < 3 || Name.Length > 100)
            erros += "'Nome' inválido! Deve ter entre 3 e 100 caracteres.\n";

        if (string.IsNullOrEmpty(Description))
            erros += "O Campo 'Descrição' é obrigatório\n";

        if (Description.Length < 5 || Description.Length > 255)
            erros += "'Descrição' inválida! Deve ter entre 5 e 255 caracteres.\n";

        if (Quantity < 0)
            erros += "'Quantidade' inválida! Deve ser um número positivo.\n";

        if (Supplier == null)
            erros += "O Campo 'Fornecedor' é obrigatório\n";

        return erros;
    }
    public void UpdateQuantity(int quantity)
    {
        Quantity += quantity;
    }
    public void SubstractQuantity(int quantity)
    {
        Quantity -= quantity;
    }
    public bool IsStockLow()
    {
        return Quantity < 20;
    }
    public bool IsSameMedication(Medication other)
    {
        return string.Equals(Name?.Trim(), other?.Name?.Trim(), StringComparison.OrdinalIgnoreCase) && Id != other.Id;
    }
}
