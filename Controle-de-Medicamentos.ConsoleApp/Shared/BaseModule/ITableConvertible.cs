namespace Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
public interface ITableConvertible
{
    /// <summary>
    /// Converte os dados da entidade em um array de strings para exibição em tabela.
    /// </summary>
    /// <returns>
    /// Um array de strings representando os campos principais da entidade, prontos para exibição formatada linha a linha.
    /// </returns>
    /// <remarks>
    /// Este método é utilizado para montar visualmente as linhas da tabela na listagem, em conjunto com os cabeçalhos definidos por <c>GetHeaders()</c>.
    /// </remarks>
    string[] ToLineStrings();
}
