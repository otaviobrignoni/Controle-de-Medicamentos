using System.Globalization;

namespace Controle_de_Medicamentos.ConsoleApp.Shared.Extensions;

public static class StringExtensions
{
    public static string ToTitleCase(this string text)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
    }
}
