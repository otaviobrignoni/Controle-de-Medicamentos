using System.Text.Json.Serialization;
using System.Text.Json;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.Shared;

public class DataContext
{
    // criar listas das entidades
    public List<Medication> Medications { get; set; }
    public List<Employee> Employees { get; set; }
    public List<Supplier> Suppliers { get; set; } 


    /// <summary>
    /// Caminho onde os dados da aplicação são salvos no formato JSON.
    /// </summary>
    /// <remarks>
    /// O caminho completo é construído dentro da pasta <c>%AppData%</c> do usuário, geralmente localizada em:
    /// <br/><c>C:\Users\SeuUsuario\AppData\Roaming\ControleDeMedicamentos</c>
    /// <br/>Esse local é usado para armazenar dados persistentes da aplicação, como registros ou configurações.
    /// </remarks>
    private string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"ControleDeMedicamentos");
    private string arquiveName = "SaveData.json";

    public DataContext()
    {
        //instanciar listas
        Medications = new List<Medication>();
        Employees = new List<Employee>();
        Suppliers = new List<Supplier>();
    }

    /// <summary>
    /// Inicializa uma nova instância da classe <c>DataContext</c> e carrega os dados salvos de um arquivo JSON local.
    /// </summary>
    /// <param name="loadData">
    /// Se <c>true</c>, carrega automaticamente os dados persistidos no disco ao instanciar o contexto.
    /// </param>
    public DataContext(bool loadData) : this() 
    {
        if (loadData) 
            LoadData();
    }

    /// <summary>
    /// Serializa a instância atual do <c>DataContext</c> em formato JSON e salva o arquivo no disco.
    /// </summary>
    /// <remarks>
    /// O caminho completo do arquivo é definido combinando <c>savePath</c> com <c>arquiveName</c>. <br/>
    /// Caso o diretório não exista, ele será criado automaticamente.
    /// A serialização utiliza formatação indentada e preserva referências para evitar ciclos de objetos.
    /// </remarks>
    public void SaveData()
    {
        string fullPath = Path.Combine(savePath, arquiveName);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        string json = JsonSerializer.Serialize(this, jsonOptions);

        if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

        File.WriteAllText(fullPath, json);
    }

    /// <summary>
    /// Carrega os dados salvos de um arquivo JSON local e os desserializa para restaurar o estado anterior do <c>DataContext</c>.
    /// </summary>
    /// <remarks>
    /// O arquivo é lido a partir do caminho definido pela combinação de <c>savePath</c> com <c>arquiveName</c>.<br/>
    /// Se o arquivo não existir ou estiver vazio, o método é encerrado sem alterar o estado atual.
    /// A desserialização utiliza <c>ReferenceHandler.Preserve</c> para manter referências circulares intactas.
    /// </remarks>
    private void LoadData()
    {
        string fullPath = Path.Combine(savePath, arquiveName);

        if (!File.Exists(fullPath)) return;

        string json = File.ReadAllText(fullPath);

        if (string.IsNullOrWhiteSpace(json)) return;

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        DataContext savedContext = JsonSerializer.Deserialize<DataContext>(json, jsonOptions)!;

        if (savedContext == null) return;

        // Carregar listas de entidades a partir do contexto salvo
        Medications = savedContext.Medications;
        Employees = savedContext.Employees;
        Suppliers = savedContext.Suppliers;
    }
}


