namespace Controle_de_Medicamentos.ConsoleApp.Utils;

public static class Validator
{
    public static int GetValidInt()
    {
        int isInt;
            while (!int.TryParse(Console.ReadLine(), out isInt))
                Write.InColor("> (X) O Valor precisa ser um número! Digite novamente: ", ConsoleColor.Red, true);
        return isInt;
    }

    public static DateTime GetValidDate()
    {
        DateTime newDate;
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out newDate))
            Write.InColor("> (X) A data precisa ser no formato \"dd/mm/yyyy\"! Digite novamente: ", ConsoleColor.Red, true);
        return newDate;
    }
}
