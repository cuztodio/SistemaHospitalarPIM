using System;

namespace SistemaHospitalar
{
    public class Receita
    {
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public string Conteudo { get; set; }
        public DateTime Data { get; set; }

        public Receita(Paciente paciente, Medico medico, string conteudo)
        {
            Paciente = paciente;
            Medico = medico;
            Conteudo = conteudo;
            Data = DateTime.Now;
        }
    }
}
