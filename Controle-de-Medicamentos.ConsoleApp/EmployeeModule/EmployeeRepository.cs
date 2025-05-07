using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(DataContext context) : base(context){}

    public override List<Employee> GetList()
    {
        return Context.Employees;
    }

    public override bool IsEntityValid(Employee entity, out string errors)
    {
        errors = entity.Validate();
        if (entity.IsSameCPF(GetAll().FirstOrDefault(e => e.IsSameCPF(entity))))
            errors += "Já existe um funcionário com este CPF";

        if (string.IsNullOrEmpty(errors))
            return true;
        return false;
    }
}
