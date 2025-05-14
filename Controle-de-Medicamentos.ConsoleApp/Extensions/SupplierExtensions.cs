using Controle_de_Medicamentos.ConsoleApp.Models;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.Extensions;

public static class SupplierExtensions
{
    public static Supplier ToEntity(this SupplierFormViewModel viewModel)
    {
        return new Supplier(viewModel.Name, viewModel.PhoneNumber, viewModel.CNPJ);
    }

    public static SupplierDetailsViewModel ToDetailsViewModel(this Supplier supplier)
    {
        return new SupplierDetailsViewModel(supplier.Id, supplier.Name, supplier.PhoneNumber, supplier.CNPJ);
    }
}
