using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
namespace Controle_de_Medicamentos.ConsoleApp.MedicationModule;

public class Medication : BaseEntity<Medication>
{
    public string Name {get; set; }
    private string Description { get; set; }
    public int Quantity { get; set; }
    private Supplier Supplier { get; set; }

    public Medication(){}

    public Medication(string name, string description, int quantity, Supplier supplier)
    {
        Name = name;
        Description = description;
        Quantity = quantity;
        Supplier = supplier;
    }

    /// <summary>
    /// Atualiza a quantidade em estoque do medicamento, substituindo o valor atual pelo informado.
    /// </summary>
    /// <param name="newQuantity">Novo valor da quantidade em estoque.</param>
    /// <remarks>
    /// Este método sobrescreve a quantidade atual.
    /// </remarks>
    public void UpdateQuantity(int newQuantity)
    {
        Quantity += newQuantity;
    }

    /// <summary>
    /// Verifica se o medicamento está com o estoque baixo, considerando o limite mínimo de 20 unidades.
    /// </summary>
    /// <returns>
    /// Retorna <c>true</c> se a quantidade em estoque for inferior a 20; caso contrário, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Esse método sera utilizado para exibir como "EM FALTA" caso atenda ao requisito.
    /// </remarks>
    public bool IsLowStock()
    {
        return Quantity < 20;
    }

    /// <summary>
    /// Compara dois medicamentos para verificar se representam o mesmo item, com base no nome.
    /// </summary>
    /// <param name="other">Outro medicamento a ser comparado.</param>
    /// <returns>
    /// Retorna <c>true</c> se os medicamentos tiverem o mesmo nome (ignorando maiúsculas, minúsculas e espaços); 
    /// caso contrário, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Esse método pode ser usado para evitar duplicidade de registros, consolidando a quantidade em estoque quando apropriado.
    /// </remarks>
    public bool IsSameMedication(Medication other)
    {
        return string.Equals(Name?.Trim(), other?.Name?.Trim(), StringComparison.OrdinalIgnoreCase);
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
            erros += "Nome inválido! Deve ter entre 3 e 100 caracteres.\n";

        if (string.IsNullOrEmpty(Description))
            erros += "O Campo 'Descrição' é obrigatório\n";

        if (Description.Length < 5 || Description.Length > 255)
            erros += "Descrição inválida! Deve ter entre 5 e 255 caracteres.\n";

        if (Quantity < 0)
            erros += "Quantidade inválida! Deve ser um número positivo.\n";

        if (Supplier == null)
            erros += "O Campo 'Fornecedor' é obrigatório\n";

        return erros;
    }
}
