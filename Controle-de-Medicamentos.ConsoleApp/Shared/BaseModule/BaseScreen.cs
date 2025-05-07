using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
using Controle_de_Medicamentos.ConsoleApp.Utils;

namespace Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

public abstract class BaseScreen<T> where T : BaseEntity<T>
{
    public IRepository<T> Repository { get; set; }
    public string EntityName { get; set; }

    public BaseScreen(IRepository<T> repository, string entityName)
    {
        Repository = repository;
        EntityName = entityName;
    }

    /// <summary>
    /// Metodo abstrato que deve ser implementado nas classes derivadas para exibir o menu principal da tela.
    /// <summary>
    public abstract void ShowMenu();

    /// <summary>
    /// Exibe um menu interativo no console com base nas opções fornecidas, permitindo navegação com as teclas.
    /// </summary>
    /// <param name="title">Título exibido no topo do menu.</param>
    /// <param name="menuOptions">Lista de opções que serão mostradas no menu.</param>
    /// <param name="executeOption">
    /// Função que será executada quando o usuário pressionar Enter.
    /// Deve retornar <c>true</c> se o menu deve ser encerrado, ou <c>false</c> para continuar exibindo.
    /// </param>
    /// <remarks>
    /// O usuário pode navegar pelas opções utilizando as setas ↑ ↓ e selecionar com Enter.
    /// Pressionar Esc encerra o menu imediatamente.
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

    /// <summary>
    /// Executa a ação correspondente à opção selecionada no menu principal.
    /// </summary>
    /// <param name="indexSelected">Índice da opção selecionada pelo usuário.</param>
    /// <returns>
    /// Retorna <c>true</c> se a opção selecionada indicar saída do menu; caso contrário, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Este método é responsável por mapear os índices do menu para métodos como <c>Add</c>, <c>Edit</c>, <c>Remove</c> e <c>ShowAll</c>.
    /// Pode ser sobrescrito para adicionar ou alterar comportamentos específicos por entidade.
    /// </remarks>
    protected virtual bool ExecuteOption(int indexSelected)
    {
        switch (indexSelected)
        {
            case 0: Add(); break;
            case 1: Edit(); break;
            case 2: Remove(); break;
            case 3: ShowAll(true, true); break;
            case 4: return true;
            default: Write.InvalidOption(); break;
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
        Write.Exit();
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
        Write.Header($" Editando {EntityName}");

        if (!ExistRegisters())
            return;

        ShowAll(false);
        Write.InColor($">> Digite o ID do {EntityName} que deseja editar: ", ConsoleColor.Yellow, true);
        int id = Convert.ToInt32(Console.ReadLine());

        if (!FindRegister(id))
            return;

        T entity = NewEntity();
        entity.Id = id;

        if (!IsValid(entity))
            return;

        Repository.Edit(id, entity);

        Write.InColor($">> (✓) {EntityName} editado com sucesso!", ConsoleColor.Green);
        Write.Exit();
    }

    /// <summary>
    /// Remove uma entidade existente com base no ID informado pelo usuário.
    /// </summary>
    /// <remarks>
    /// Este método limpa o console e exibe o cabeçalho com o nome da entidade. <br/>
    /// Em seguida:
    /// <list type="number">
    /// <item>Verifica se existem registros com <see cref="ExistRegisters"/>.</item>
    /// <item>Exibe os itens disponíveis com <see cref="ShowAll"/>.</item>
    /// <item>Solicita o ID da entidade a ser removida.</item>
    /// <item>Verifica se o item existe com <see cref="FindRegister(int)"/>.</item>
    /// <item>Valida se a remoção é permitida com <see cref="CanRemove(int)"/>.</item>
    /// <item>Se tudo for válido, remove a entidade e exibe uma mensagem de sucesso.</item>
    /// </list>
    /// </remarks>
    public virtual void Remove()
    {
        Console.Clear();
        Write.Header($" Removendo {EntityName}");

        if (!ExistRegisters())
            return;

        ShowAll(false);
        Write.InColor($">> Digite o ID do {EntityName} que deseja remover: ", ConsoleColor.Yellow, true);
        int id = Convert.ToInt32(Console.ReadLine());

        if (!FindRegister(id))
            return;

        if (!CanRemove(id))
            return;

        Repository.Remove(id);

        Write.InColor($">> (✓) {EntityName} removido com sucesso!", ConsoleColor.Green);
        Write.Exit();
    }

    /// <summary>
    /// Verifica se a entidade com o ID informado pode ser removida.
    /// </summary>
    /// <param name="id">ID da entidade a ser verificada.</param>
    /// <returns>
    /// <c>true</c> se a remoção for permitida; caso contrário, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Este método pode ser sobrescrito nas telas específicas para aplicar regras de negócio.
    /// Exemplo: impedir exclusão de medicamentos com requisições vinculadas, ou fornecedores vinculados a medicamentos.
    /// </remarks>
    public virtual bool CanRemove(int id)
    {
        return true;
    }

    /// <summary>
    /// Exibe todos os registros da entidade em formato tabular.
    /// </summary>
    /// <param name="showExit">Se <c>true</c>, exibe mensagem de retorno ao final.</param>
    /// <param name="useClear">Se <c>true</c>, limpa o console antes da exibição.</param>
    /// <remarks>
    /// Usa <see cref="ITableConvertible.ToLineStrings"/> para renderizar os dados em tabela com bordas.
    /// </remarks>
    public virtual void ShowAll(bool showExit, bool useClear = false)
    {
        if (useClear)
            Console.Clear();

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

        int fullWidth = widths.Sum() + widths.Length * 3 - 1;

        string sufixo = typeof(T) == typeof(Supplier) ? "es" : "s";
        Write.CustomHeader($" Listando {EntityName}{sufixo}", fullWidth);

        PrintTopBorder(widths);
        PrintRow(headers, widths);
        PrintSeparator(widths);

        foreach (string[] line in lines)
            PrintRow(line, widths);

        PrintBottomBorder(widths);

        if (showExit)
            Write.Exit();       
    }

    /// <summary>
    /// Retorna os títulos das colunas exibidas na listagem.
    /// </summary>
    /// <returns>Array com os nomes das colunas.</returns>
    public abstract string[] GetHeaders();

    /// <summary>
    /// Imprime a borda superior da tabela com base na largura das colunas.
    /// </summary>
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

    /// <summary>
    /// Imprime a linha separadora entre o cabeçalho e os dados.
    /// </summary>
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

    /// <summary>
    /// Imprime a borda inferior da tabela.
    /// </summary>
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

    /// <summary>
    /// Imprime uma linha da tabela com espaçamento proporcional à largura de cada coluna.
    /// </summary>
    /// <param name="row">Valores da linha.</param>
    /// <param name="widths">Largura máxima de cada coluna.</param>
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
            Write.Exit();
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
            Write.Exit();
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

