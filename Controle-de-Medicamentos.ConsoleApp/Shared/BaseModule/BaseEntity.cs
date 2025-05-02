namespace Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

public abstract class BaseEntity<T>
{
    public int Id { get; set; }

    /// <summary>
    /// Atualiza os dados da entidade com base nas informações fornecidas pelo usuário.
    /// </summary>
    /// <param name="entity">Instância da entidade que será modificada.</param>
    /// <remarks>
    /// Este método deve ser implementado nas classes derivadas para aplicar atualizações específicas nos campos da entidade.
    /// </remarks>
    public abstract void UpdateEntity(T entity);

    /// <summary>
    /// Valida os dados da entidade e retorna uma string contendo os erros encontrados, se houver.
    /// </summary>
    /// <returns>
    /// Uma string com as mensagens de erro de validação. Se a entidade for válida, retorna uma string vazia.
    /// </returns>
    /// <remarks>
    /// Este método deve ser implementado nas classes derivadas com regras específicas de validação da entidade.
    /// Ele deve ser utilizada antes de salvar ou editar a entidade, garantindo integridade dos dados.
    /// </remarks>
    public abstract string Validate();
}
