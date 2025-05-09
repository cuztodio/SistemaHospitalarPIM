using System;

namespace SistemaHospitalar
{
    public class Medico : Pessoa
    {
        public string Crm { get; set; }
        public string Especialidade { get; set; }
        public string Departamento { get; set; }

        public Medico(
            string nome,
            string cpf,
            DateTime dataNascimento,
            string email,
            string usuario,
            string senha,
            string crm,
            string especialidade,
            string departamento
        ) : base(nome, cpf, dataNascimento, email, usuario, senha)
        {
            Crm = crm;
            Especialidade = especialidade;
            Departamento = departamento;
        }

        public override void ExibirDados()
        {
            base.ExibirDados();
            Console.WriteLine($"CRM: {Crm}");
            Console.WriteLine($"Especialidade: {Especialidade}");
            Console.WriteLine($"Departamento: {Departamento}");
        }
    }
}
