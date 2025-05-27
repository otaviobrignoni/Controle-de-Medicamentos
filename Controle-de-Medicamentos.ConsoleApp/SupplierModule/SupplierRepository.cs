using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.SupplierModule
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DataContext context) : base(context) { }

        public override List<Supplier> GetList()
        {
            return Context.Suppliers;
        }

        public override bool IsEntityValid(Supplier entity, out string errors)
        {
            errors = entity.Validate();

            if (entity.IsSameCNPJ(GetAll().FirstOrDefault(s => s.IsSameCNPJ(entity))))
                errors += "Já existe um fornecedor com este CNPJ";

            if (string.IsNullOrEmpty(errors))
                return true;
            return false;
        }
    }
}

