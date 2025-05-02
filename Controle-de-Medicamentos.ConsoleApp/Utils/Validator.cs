namespace Controle_de_Medicamentos.ConsoleApp.Utils;

public static class Validator
{
    public static int GetValidInt()
    {
        int isInt;
            while (!int.TryParse(Console.ReadLine(), out isInt))
                Write.WriteInColor("> (X) O Valor precisa ser um número! Digite novamente: ", ConsoleColor.Red, true);

        return isInt;
    }
}
