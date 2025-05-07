namespace Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

public abstract class BaseRepository<T> where T : BaseEntity<T>
{
    protected List<T> List = new List<T>();
    protected DataContext Context { get; set; }

    public BaseRepository(DataContext context)
    {
        Context = context;
        List = GetList();
    }

    /// <summary>
    /// Obtém a lista de registros do repositório.
    /// Deve ser implementado pelas classes derivadas para retornar os dados persistidos.
    /// </summary>
    public abstract List<T> GetList();

    /// <summary>
    /// Adiciona uma nova entidade à lista e persiste a alteração no arquivo JSON.
    /// </summary>
    /// <param name="entity">Entidade a ser adicionada.</param>
    /// <remarks>
    /// A entidade recebe automaticamente o próximo ID disponível por meio de <see cref="GetNextAvailableId"/>.
    /// Após a inserção, os dados são salvos no disco utilizando <see cref="Context.SaveData"/>.
    /// </remarks>
    public virtual void Add(T entity)
    {
        entity.Id = GetNextAvailableId();
        List.Add(entity);
        Context.SaveData(); 
    }

    /// <summary>
    /// Remove a entidade correspondente ao ID informado da lista e atualiza o arquivo de dados.
    /// </summary>
    /// <param name="id">ID da entidade que será removida.</param>
    /// <remarks>
    /// A entidade é localizada com <see cref="GetById(int)"/> e removida da lista.
    /// Após a remoção, a persistência é feita com <see cref="Context.SaveData"/>.
    /// </remarks>
    public virtual void Remove(int id)
    {
        T entity = GetById(id);
        List.Remove(entity);
        Context.SaveData();
    }

    /// <summary>
    /// Atualiza uma entidade existente com os dados fornecidos e salva a modificação no arquivo JSON.
    /// </summary>
    /// <param name="id">ID da entidade que será atualizada.</param>
    /// <param name="editedEntity">Entidade contendo os novos dados.</param>
    /// <remarks>
    /// A entidade original é obtida com <see cref="GetById(int)"/> e atualizada usando <see cref="UpdateEntity"/>.
    /// Após a modificação, os dados são persistidos com <see cref="Context.SaveData"/>.
    /// </remarks>
    public virtual void Edit(int id, T editedEntity)
    {
        T entity = GetById(id);
        entity.UpdateEntity(editedEntity);
        Context.SaveData();
    }

    /// <summary>
    /// Seleciona todos os registros da lista.
    /// </summary>
    /// <returns>Retorna a lista de registros</returns>
    public virtual List<T> GetAll()
    {
        return List;
    }

    /// <summary>
    /// Busca a entidade correspondente ao ID informado, caso exista na lista.
    /// </summary>
    /// <param name="id">ID da entidade a ser buscada.</param>
    /// <returns>Objeto da entidade encontrada ou <c>null</c> caso não exista.</returns>
    public T? GetById(int id)
    {
        return List.FirstOrDefault(targetEntity => targetEntity.Id == id);
    }

    /// <summary>
    /// Busca o ID disponível que não esteja presente na lista atual de entidades.<br/>
    /// Para quando há buracos na sequência de IDs existentes (ex: exclusões).
    /// </summary>
    /// <returns>O menor ID disponível que ainda não foi utilizado.</returns>
    public int GetNextAvailableId()
    {
        int id = 1;
        List<int> usedIds = List.Select(baseEntity => baseEntity.Id).OrderBy(id => id).ToList();
        foreach (int usedId in usedIds)
        {
            if (usedId != id)
                break;
            id++;
        }
        return id;
    }

    /// <summary>
    /// Verificar se a Entidade é válida.<br/>
    /// Acrescenta a uma string os motivos pelos quais a entidade não pôde ser validada.<br/>
    /// </summary>
    /// <param name="entity">Entidade a ser validada.</param>
    /// <param name="errors">Retorna uma string com os erros encontrados, caso existam para exibir ao usuário</param>
    /// <returns>Retorna <c>true</c> se a entidade for válida; caso contrário, <c>false</c>.</returns>
    public virtual bool IsEntityValid(T entity, out string errors)
    {
        errors = entity.Validate();
        if (string.IsNullOrEmpty(errors))
            return true;
        return false;
    }

    /// <summary>
    /// Conta os registros na lista.<br/>
    /// </summary>
    /// <returns> Retorna o número de registros na lista.</returns>
    public int Count()
    {
        return List.Count;
    }
}
