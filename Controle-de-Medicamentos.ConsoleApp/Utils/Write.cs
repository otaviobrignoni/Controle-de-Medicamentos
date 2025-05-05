namespace Controle_de_Medicamentos.ConsoleApp.Utils;

public static class Write
{
    private const int MenuWidth = 40;

    /// <summary>
    /// Escreve uma mensagem no console com a cor especificada.
    /// </summary>
    /// <param name="message">Texto a ser exibido no console.</param>
    /// <param name="cor">Cor do texto a ser utilizada durante a escrita.</param>
    /// <param name="write">
    /// Define o tipo de escrita:
    /// <c>true</c> para <see cref="Console.Write"/>, <c>false</c> para <see cref="Console.WriteLine"/>.
    /// </param>
    public static void InColor(string message, ConsoleColor cor, bool write = false)
    {
        Console.ForegroundColor = cor;
        if (write)
            Console.Write(message);
        else
            Console.WriteLine(message);
        Console.ResetColor();
    }

    /// <summary>
    /// Exibe uma mensagem instruindo o usuário a pressionar Enter para continuar, e aguarda a entrada do usuário.
    /// </summary>
    public static void ShowExit()
    {
        Console.WriteLine();
        InColor(">> Pressione [Enter] para continuar.", ConsoleColor.DarkYellow, true);
        Console.ReadKey();
    }

    /// <summary>
    /// Exibe uma mensagem de erro informando que a opção selecionada é inválida e aguarda o usuário pressionar Enter.
    /// </summary>
    /// <remarks>
    /// A mensagem é exibida em vermelho para destacar o erro, seguida de uma pausa com <see cref="Console.ReadKey"/> antes de retornar ao fluxo do sistema.
    /// </remarks>
    public static void ShowInvalidOption()
    {
        Console.WriteLine();
        InColor(">> (X) Opção inválida, pressione [Enter] para tentar novamente.", ConsoleColor.Red, true);
        Console.ReadKey();
    }

    /// <summary>
    /// Exibe uma mensagem instruindo o usuário a pressionar Enter para tentar novamente, e aguarda a entrada do usuário.
    /// </summary>
    /// <remarks>
    /// A mensagem é exibida em vermelho para destacar o erro, seguida de uma pausa com <see cref="Console.ReadKey"/> antes de retornar ao fluxo do sistema.
    /// </remarks>
    public static void ShowTryAgain()
    {
        Console.WriteLine();
        InColor(">> Pressione [Enter] para tentar novamente.", ConsoleColor.Yellow, true);
        Console.ReadKey();
    }

    /// <summary>
    /// Exibe um cabeçalho formatado com bordas no console, centralizando o título informado.
    /// </summary>
    /// <param name="title">Texto a ser exibido como título no cabeçalho.</param>
    /// <remarks>
    /// A largura do cabeçalho é determinada pela constante <c>MenuWidth</c>.
    /// </remarks>
    public static void Header(string title)
    {
        int padding = (MenuWidth - title.Length) / 2;
        InColor("┌" + new string('─', MenuWidth) + "┐", ConsoleColor.Blue);
        InColor("│" + title.PadLeft(padding + title.Length).PadRight(MenuWidth) + "│", ConsoleColor.Blue);
        InColor("└" + new string('─', MenuWidth) + "┘", ConsoleColor.Blue);
    }

    /// <summary>
    /// Exibe um cabeçalho formatado no console com bordas, centralizando o título de acordo com a largura personalizada informada.
    /// </summary>
    /// <param name="title">Texto a ser exibido como título no cabeçalho.</param>
    /// <param name="customWidth">Largura total do cabeçalho, usada para formatar as bordas e centralizar o texto.</param>
    public static void Header(string title, int customWidth)
    {
        int padding = (customWidth - title.Length) / 2;
        Console.WriteLine("┌" + new string('─', customWidth) + "┐");
        Console.WriteLine("│" + title.PadLeft(padding + title.Length).PadRight(customWidth) + "│");
        Console.WriteLine("├" + new string('─', customWidth) + "┤");
    }
}
