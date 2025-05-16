using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.Models;

public abstract class MedicationFormViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public int SupplierId { get; set; }

    public List<SelectSupplierViewModel> AvailableSuppliers { get; set; }

    protected MedicationFormViewModel()
    {
        AvailableSuppliers = new List<SelectSupplierViewModel>();
    }
}

public class SelectSupplierViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public SelectSupplierViewModel(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class AddMedicationViewModel : MedicationFormViewModel
{
    public AddMedicationViewModel()
    {
    }
    public AddMedicationViewModel(List<Supplier> suppliers) : this()
    {
        foreach (var supplier in suppliers)
        {
            var selectViewModel = new SelectSupplierViewModel(supplier.Id, supplier.Name);
            AvailableSuppliers.Add(selectViewModel);
        }
    }
}

public class EditMedicationViewModel : MedicationFormViewModel
{
    public int Id { get; set; }
    public EditMedicationViewModel()
    {
    }
    public EditMedicationViewModel(int id, string name, string description, int quantity, int supplierId, List<Supplier> suppliers)
    {
        Id = id;
        Name = name;
        Description = description;
        Quantity = quantity;
        SupplierId = supplierId;
        foreach (var supplier in suppliers)
        {
            var selectViewModel = new SelectSupplierViewModel(supplier.Id, supplier.Name);
            AvailableSuppliers.Add(selectViewModel);
        }
    }
}

public class RemoveMedicationViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public RemoveMedicationViewModel() { }
    public RemoveMedicationViewModel(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class ShowMedicationsViewModel
{
    public List<MedicationDetailsViewModel> List { get; } = new();
    public ShowMedicationsViewModel(List<Medication> medications)
    {
        foreach (Medication m in medications)
        {
            MedicationDetailsViewModel detailsViewModel = m.ToDetailsViewModel();
            List.Add(detailsViewModel);
        }
    }
}

public class MedicationDetailsViewModel
{
    public int Id;
    public string Name;
    public string Description;
    public int Quantity;
    public string SupplierName;

    public MedicationDetailsViewModel(int id, string name, string description, int quantity, Supplier supplier)
    {
        Id = id;
        Name = name;
        Description = description;
        Quantity = quantity;
        SupplierName = supplier.Name;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Nome: {Name}, Descrição: {Description}, Quantidade: {Quantity}, Fornecedor: {SupplierName}";
    }
}