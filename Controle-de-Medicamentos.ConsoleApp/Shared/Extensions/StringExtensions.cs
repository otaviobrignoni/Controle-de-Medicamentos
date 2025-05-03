using System.Globalization;

namespace Controle_de_Medicamentos.ConsoleApp.Shared.Extensions;

public static class StringExtensions
{
    // Usando this torna o metodo uma extensão para ser usada em toda string
    public static string ToTitleCase(this string text)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
    }
}
