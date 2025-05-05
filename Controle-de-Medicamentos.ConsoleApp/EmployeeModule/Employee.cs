using System.Text.RegularExpressions;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
public class Employee : BaseEntity<Employee>, ITableConvertible
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string CPF { get; set; }

    public Employee() { }

    public Employee(string name, string phoneNumber, string cpf)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        CPF = cpf;
    }

    public override void UpdateEntity(Employee entity)
    {
        Name = entity.Name;
        PhoneNumber = entity.PhoneNumber;
        CPF = entity.CPF;
    }

    public override string Validate()
    {
        string errors = "";

        if (string.IsNullOrEmpty(Name))
            errors += "O Campo \"Nome\" é obrigatório\n";
        if (Name.Length < 3 || Name.Length > 100)
            errors += "Nome inválido! Deve ter entre 3 e 100 caracteres.\n";
        if (string.IsNullOrEmpty(PhoneNumber))
            errors += "O Campo \"Telefone\" é obrigatório\n";
        if (!Regex.IsMatch(PhoneNumber, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
            errors += "Telefone inválido! Deve estar no formato (DDD) XXXX-XXXX ou (DDD) XXXXX-XXXX\n";
        if (string.IsNullOrEmpty(CPF))
            errors += "O Campo \"CPF\" é obrigatório\n";
        if (CPF.Length != 11)
            errors += "CPF Inválido! Deve ter exatamente 11 números\n";

        return errors;
    }

    public string[] ToLineStrings()
    {
        return new string[] { Id.ToString(), Name, PhoneNumber, CPF };
    }

    public bool IsSameCPF(Employee employee)
    {
        return string.Equals(CPF?.Trim(), employee?.CPF?.Trim(), StringComparison.OrdinalIgnoreCase);
    }
}
