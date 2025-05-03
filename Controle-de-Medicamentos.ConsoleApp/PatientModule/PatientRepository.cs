using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.PatientModule
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(DataContext context) : base (context) {}

        public override List<Patient> GetList()
        {
            return Context.Patients;
        }

        public override bool IsEntityValid(Patient entity, out string errors)
        {
            errors = entity.Validate();

            if(entity.IsSameSUSCard(GetAll().FirstOrDefault(p=>p.IsSameSUSCard(entity))))
                errors += "Já existe um paciente com este cartão do SUS";

            if (string.IsNullOrEmpty(errors))
                return true;
            return false;
        }
    }
}
