namespace Controle_de_Medicamentos.ConsoleApp.SupplierModule;

public class Supplier : BaseEntity<Supplier>
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string CNPJ { get; set; }

    public Supplier() { }

    public Supplier(string name, string phone, string cnpj)
    {
        Name = name;
        Phone = phone;
        CNPJ = cnpj;    
    }

    public override void UpdateEntity(Supplier entity)
    {
        Name = entity.Name;
        Phone = entity.Phone;
        CNPJ = entity.CNPJ; 
    }

    public override string Validate()
    {
        string erros = "";

        if (string.IsNullOrEmpty(Name))
            erros += "O Campo 'Nome' é obrigatório\n";

        if(Name.Length < 3 || Name.Length > 100)
            erros += "Nome inválido! Deve ter entre 3 e 100 caracteres.\n";
        
        if (string.IsNullOrEmpty(Phone))
            erros += "O Campo 'Telefone' é obrigatório\n";
        
        if (!Regex.IsMatch(Phone, @"^(\d{2}) \d{4,5}-\d{4}$"))
            erros += "O Telefone deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX\n";

        if (string.IsNullOrEmpty(CNPJ))
            erros += "O Campo 'CNPJ' é obrigatório\n";

        if (CNPJ.Length != 14)
            erros+= "O CNPJ deve ter 14 dígitos\n";
        return erros;
    }


}
