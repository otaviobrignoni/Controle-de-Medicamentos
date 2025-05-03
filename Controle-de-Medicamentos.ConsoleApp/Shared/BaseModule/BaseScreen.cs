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

    /// <summary>
    /// Exibe o menu principal da entidade, permitindo a navegação entre as operações disponíveis.
    /// </summary>
    /// <remarks>
    /// Este método deve ser implementado nas classes derivadas para definir as opções específicas de interação com a entidade. <br/>
    /// Cada opção do menu executa ações correspondentes.
    /// </remarks>
    public abstract void ShowMenu();

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
        Write.Header($" Registrando {EntityName}s");
        T newEntity = NewEntity();

        if (!IsValid(newEntity))
            return;
       
        Repository.Add(newEntity);

        Write.InColor($">> (✓) {EntityName} registrado com sucesso!", ConsoleColor.Green);

        Write.ShowExitMessage();
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
        Write.ShowExitMessage();

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
        Write.ShowExitMessage();
    }

    /// <summary>
    /// Exibe todos os registros da entidade, com cabeçalho e linhas formatadas.
    /// </summary>
    /// <param name="showExit">
    /// Define se uma mensagem de encerramento ("Pressione Enter para voltar") será exibida ao final da listagem.
    /// </param>
    /// <remarks>
    /// O método utiliza <see cref="ExistRegisters"/> para verificar se há dados antes da exibição.<br/><br/>
    /// O cabeçalho da tabela e as linhas são desenhados utilizando os métodos <see cref="ShowTableHeader"/> e <see cref="ShowTableRow(T)"/>.
    /// </remarks>
    public virtual void ShowAll(bool showExit, bool useClear = false)
    {
        if(useClear)
            Console.Clear();
        Write.Header($" Listando {EntityName}s");

        if (!ExistRegisters())
            return;

        ShowTableHeader();

        foreach (T entity in Repository.GetAll())
        {
            ShowTableRow(entity);
        }

        if (showExit) 
            Write.ShowExitMessage();
    }

    /// <summary>
    /// Desenha o cabeçalho da tabela no console, representando os títulos das colunas.
    /// </summary>
    /// <remarks>
    /// Este método deve ser implementado nas classes filhas para definir os títulos conforme o modelo da entidade.
    /// </remarks>
    protected abstract void ShowTableRow(T entity);

    /// <summary>
    /// Desenha uma linha da tabela no console com os dados de uma entidade específica.
    /// </summary>
    /// <param name="entity">Instância da entidade cujos dados serão exibidos na linha.</param>
    /// <remarks>
    /// Este método deve ser implementado nas classes filhas para formatar os dados da entidade conforme desejado.
    /// </remarks>
    protected abstract void ShowTableHeader();

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
            Write.InColor(errors, ConsoleColor.Red, true);
            Write.ShowExitMessage();
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
            Write.ShowExitMessage();
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
        {
            Write.InColor($">> (×) {EntityName} não encontrado!", ConsoleColor.Red);
            Write.ShowExitMessage();
            return false;
        }
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

