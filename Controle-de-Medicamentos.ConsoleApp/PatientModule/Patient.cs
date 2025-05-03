using System.Text.RegularExpressions;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.PatientModule
{
    public class Patient : BaseEntity<Patient>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string SUSCard { get; set; }

        public Patient() {}

        public Patient(string name, string phoneNumber, string susCard)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            SUSCard = susCard;
        }

        public override void UpdateEntity(Patient entity)
        {
            Name = entity.Name;
            PhoneNumber = entity.PhoneNumber;
            SUSCard = entity.SUSCard;
        }

        public override string Validate()
        {
            string erros = "";

            if (string.IsNullOrEmpty(Name))
                erros += "O Campo 'Nome' é obrigatório\n";

            if (Name.Length < 3 || Name.Length > 100)
                erros += "Nome inválido! Deve ter entre 3 e 100 caracteres.\n";

            if (string.IsNullOrEmpty(PhoneNumber))
                erros += "O Campo 'Telefone' é obrigatório\n";

            if (!Regex.IsMatch(PhoneNumber, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
                erros += "O Telefone deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX\n";

            if (string.IsNullOrEmpty(SUSCard))
                erros += "O Campo 'Cartão do SUS' é obrigatório\n";

            if (SUSCard.Length != 15)
                erros += "O Cartão do SUS deve ter 15 dígitos\n";

            return erros;
        }
        public bool IsSameSUSCard(Patient patient)
        {
            return string.Equals(SUSCard?.Trim(), patient?.SUSCard?.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        internal bool IsSamePatient(object value)
        {
            throw new NotImplementedException();
        }
    }
}

