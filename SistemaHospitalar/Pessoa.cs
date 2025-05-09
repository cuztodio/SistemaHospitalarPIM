using System;

namespace SistemaHospitalar
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public Pessoa(string nome, string cpf, DateTime dataNascimento, string email, string usuario, string senha)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Email = email;
            Usuario = usuario;
            Senha = senha;
        }

        public virtual void ExibirDados()
        {
            Console.WriteLine($"Nome: {Nome}");
            Console.WriteLine($"CPF: {Cpf}");
            Console.WriteLine($"Data de Nascimento: {DataNascimento.ToShortDateString()}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Usuário: {Usuario}");
        }
    }
}
