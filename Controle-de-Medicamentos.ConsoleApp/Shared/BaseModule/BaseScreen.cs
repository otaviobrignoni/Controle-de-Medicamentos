using Controle_de_Medicamentos.ConsoleApp.Utils;

namespace Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

public abstract class BaseScreen<T> where T : BaseEntity<T>
{
    public IRepository<T> Repository { get; set; }
    public string EntityName { get; set; }

    public BaseScreen(IRepository<T> repository, string EntityName)
    {
        Repository = repository;
        this.EntityName = EntityName;
    }

    public abstract void ShowMenu();

    /// <summary>
    /// Exibe o menu principal da entidade, permitindo a navegação entre as operações disponíveis.
    /// </summary>
    /// <remarks>
    /// Este método deve ser implementado nas classes derivadas para definir as opções específicas de interação com a entidade. <br/>
    /// Cada opção do menu executa ações correspondentes.
    /// </remarks>
    protected virtual void ShowMenu(string title, string[] menuOptions, Func<int, bool> executeOption) 
    {
        int indexSelected = 0;
        ConsoleKey key;

        do {
            Console.Clear();
            Write.Header(title);
            Console.WriteLine();

            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == indexSelected)
                    Write.InColor($"-> {menuOptions[i]}", ConsoleColor.Green);
                else
                    Console.WriteLine($"   {menuOptions[i]}");
            }

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow: indexSelected = (indexSelected == 0) ? menuOptions.Length - 1 : indexSelected - 1; break;

                case ConsoleKey.DownArrow: indexSelected = (indexSelected + 1) % menuOptions.Length; break;

                case ConsoleKey.Enter: bool exit = executeOption(indexSelected);
                    if (exit) return; break;

                case ConsoleKey.Escape: return;
            }
        } while (true);
    }

    protected virtual bool ExecuteOption(int indexSelected)
    {
        switch (indexSelected)
        {
            case 0: Add(); break;
            case 1: Edit(); break;
            case 2: Remove(); break;
            case 3: ShowAll(true, true); break;
            case 4: return true;
            default: Write.ShowInvalidOption(); break;
        }
        return false;
    }

    /// <summary>
    /// Cadastra uma nova entidade a partir dos dados inseridos pelo usuário.
    /// </summary>
    /// <remarks>
    /// O método chama <see cref="NewEntity"/> para obter os dados da entidade, valida com <see cref="IsValid(T)"/> e,
    /// se válida, adiciona ao repositório e exibe uma mensagem de sucesso no console.
    /// </remarks>
    public virtual void Add()
    {
        Console.Clear();
        Write.Header($" Registrando {EntityName}");
        T newEntity = NewEntity();

        if (!IsValid(newEntity))
            return;
       
        Repository.Add(newEntity);

        Write.InColor($">> (✓) {EntityName} registrado com sucesso!", ConsoleColor.Green);

        Write.ShowExit();
    }

    /// <summary>
    /// Permite ao usuário editar uma entidade existente, substituindo seus dados.
    /// </summary>
    /// <remarks>
    /// O método verifica se existem registros com <see cref="ExistRegisters"/> e exibe todos os existentes com <see cref="ShowAll"/>. <br/>
    /// Em seguida, solicita o ID do item a ser editado, valida sua existência com <see cref="FindRegister(int)"/>, 
    /// coleta os novos dados via <see cref="NewEntity"/> e atualiza o item no repositório.
    /// </remarks>
    public virtual void Edit()
    {
        Console.Clear();
        Write.Header($" Editando {EntityName}s");

        if (!ExistRegisters())
            return;

        ShowAll(false);
        Write.InColor($">> Digite o ID do {EntityName} que deseja editar:", ConsoleColor.Yellow, true);
        int id = Convert.ToInt32(Console.ReadLine());

        if (!FindRegister(id))
            return;

        T entity = NewEntity();

        if (!IsValid(entity))
            return;

        Repository.Edit(id, entity);

        Write.InColor($">> (✓) {EntityName} editado com sucesso!", ConsoleColor.Green);
        Write.ShowExit();

    }

    /// <summary>
    /// Remove uma entidade existente com base no ID informado pelo usuário.
    /// </summary>
    /// <remarks>
    /// O método verifica se existem registros com <see cref="ExistRegisters"/> e exibe todos com <see cref="ShowAll"/>. <br/>
    /// Em seguida, solicita o ID do item a ser removido, verifica sua existência com <see cref="FindRegister(int)"/>,
    /// e remove o item do repositório. Ao final, exibe uma mensagem de confirmação.
    /// </remarks>
    public virtual void Remove()
    {
        Console.Clear();
        Write.Header($" Removendo {EntityName}s");

        if (!ExistRegisters())
            return;

        ShowAll(false);
        Write.InColor($">> Digite o ID do {EntityName} que deseja remover:", ConsoleColor.Yellow, true);
        int id = Convert.ToInt32(Console.ReadLine());

        if (!FindRegister(id))
            return;

        Repository.Remove(id);

        Write.InColor($">> (✓) {EntityName} removido com sucesso!", ConsoleColor.Green);
        Write.ShowExit();
    }

    /// <summary>
    /// Exibe todos os registros da entidade, com cabeçalho e linhas formatadas.
    /// </summary>
    /// <param name="showExit">
    /// Define se uma mensagem de encerramento ("Pressione Enter para voltar") será exibida ao final da listagem.
    /// </param>
    /// <remarks>
    /// O método utiliza <see cref="ExistRegisters"/> para verificar se há dados antes da exibição.<br/><br/>
    /// O cabeçalho da tabela e as linhas são desenhados utilizando os métodos <see cref="ShowTableHeader"/> e <see cref="PrintRow(T)"/>.
    /// </remarks>

    public virtual void ShowAll(bool showExit, bool useClear = false)
    {
        if (useClear)
            Console.Clear();

        Write.Header($" Listando {EntityName}s");

        if (!ExistRegisters())
            return;

        string[] headers = GetHeaders();

        List<T> entities = Repository.GetAll();
        List<string[]> lines = new List<string[]>();
        foreach (T entity in entities)
        {
            ITableConvertible convertible = entity as ITableConvertible;
            if (convertible != null)
                lines.Add(convertible.ToLineStrings());
        }

        int[] widths = new int[headers.Length];
        for (int column = 0; column < headers.Length; column++)
        {
            int maxLength = headers[column].Length;
            foreach (string[] line in lines)
            {
                int length = line[column].Length;
                if (length > maxLength)
                    maxLength = length;
            }
            widths[column] = maxLength;
        }

        PrintTopBorder(widths);
        PrintRow(headers, widths);
        PrintSeparator(widths);

        foreach (string[] line in lines)
            PrintRow(line, widths);

        PrintBottomBorder(widths);

        if (showExit)
            Write.ShowExit();
    }

    public abstract string[] GetHeaders();

    protected void PrintTopBorder(int[] widths)
    {
        Console.Write("┌");
        for (int i = 0; i < widths.Length; i++)
        {
            Console.Write(new string('─', widths[i] + 2));
            Console.Write(i < widths.Length - 1 ? "┬" : "┐");
        }
        Console.WriteLine();
    }

    protected void PrintSeparator(int[] widths)
    {
        Console.Write("├");
        for (int i = 0; i < widths.Length; i++)
        {
            Console.Write(new string('─', widths[i] + 2));
            Console.Write(i < widths.Length - 1 ? "┼" : "┤");
        }
        Console.WriteLine();
    }

    protected void PrintBottomBorder(int[] widths)
    {
        Console.Write("└");
        for (int i = 0; i < widths.Length; i++)
        {
            Console.Write(new string('─', widths[i] + 2));
            Console.Write(i < widths.Length - 1 ? "┴" : "┘");
        }
        Console.WriteLine();
    }

    public virtual void PrintRow(string[] row, int[] widths)
    {
        Console.Write("│");
        for (int i = 0; i < row.Length; i++)
        {
            string padded = row[i].PadRight(widths[i]);
            Console.Write(" " + padded + " │");
        }
        Console.WriteLine();
    }
 

    /// <summary>
    /// Valida a entidade informada utilizando o repositório e exibe os erros no console, caso existam.
    /// </summary>
    /// <param name="newEntity">Entidade que será validada.</param>
    /// <returns>
    /// Retorna <c>true</c> se a entidade for válida; caso contrário, exibe os erros no console e retorna <c>false</c>.
    /// </returns>
    public bool IsValid(T newEntity)
    {
        if (!Repository.IsEntityValid(newEntity, out string errors))
        {
            Write.InColor($">> (×) Erro ao cadastrar {EntityName}!", ConsoleColor.Red);
            Write.InColor(errors, ConsoleColor.Red, true);
            Write.ShowExit();
            return false;
        }
        return true;
    }

    /// <summary>
    /// Verifica se existem registros da entidade no repositório.
    /// </summary>
    /// <returns>
    /// Retorna <c>true</c> se existirem registros; caso contrário, exibe uma mensagem de aviso e retorna <c>false</c>.
    /// </returns>
    public bool ExistRegisters()
    {
        if (Repository.Count() == 0)
        {
            Write.InColor($">> (×) Não há {EntityName}s cadastrados!", ConsoleColor.Red);
            Write.ShowExit();
            return false;
        }
        return true;
    }

    /// <summary>
    /// Verifica se existe um registro com o ID informado.
    /// </summary>
    /// <param name="id">ID da entidade a ser buscada.</param>
    /// <returns>
    /// Retorna <c>true</c> se o registro for encontrado; caso contrário, exibe uma mensagem de erro e retorna <c>false</c>.
    /// </returns>
    public bool FindRegister(int id)
    {
        if (Repository.GetById(id) == null)
            return false;
        return true;
    }

    /// <summary>
    /// Cria uma entidade, preenchida com os dados fornecidos pelo usuário.
    /// </summary>
    /// <returns>Retorna uma instância da entidade para ser cadastrada.</returns>
    /// <remarks>
    /// Este método deve ser implementado nas classes derivadas para capturar os dados da entidade por meio de entrada do usuário.
    /// </remarks>
    protected abstract T NewEntity();
}

