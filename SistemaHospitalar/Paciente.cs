using System;

namespace SistemaHospitalar
{
    public class Paciente : Pessoa
    {
        public string NumeroProntuario { get; set; }

        public Paciente(string nome, string cpf, DateTime dataNascimento, string email, string usuario, string senha, string numeroProntuario)
            : base(nome, cpf, dataNascimento, email, usuario, senha)
        {
            NumeroProntuario = numeroProntuario;
        }

        public override void ExibirDados()
        {
            base.ExibirDados();
            Console.WriteLine($"Número do Prontuário: {NumeroProntuario}");
        }
    }
}
