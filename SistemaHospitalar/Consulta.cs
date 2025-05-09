using System;

namespace SistemaHospitalar
{
    public class Consulta
    {
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public DateTime DataHora { get; set; }

        public Consulta(Paciente paciente, Medico medico, DateTime dataHora)
        {
            Paciente = paciente;
            Medico = medico;
            DataHora = dataHora;
        }

        public void ExibirConsulta()
        {
            Console.WriteLine($"Médico: {Medico.Nome}");
            Console.WriteLine($"Especialidade: {Medico.Especialidade}");
            Console.WriteLine($"Data e Hora: {DataHora.ToString("dd/MM/yyyy HH:mm")}");
        }
    }
}
