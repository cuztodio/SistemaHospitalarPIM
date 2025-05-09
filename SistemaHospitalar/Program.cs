using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaHospitalar
{
    class Program
    {
        static List<Pessoa> pessoas = new List<Pessoa>();
        static List<Consulta> consultas = new List<Consulta>();
        static List<Receita> receitas = new List<Receita>();


        static void Main(string[] args)
        {
            // Cadastro de exemplo
            pessoas.Add(new Medico("Dr. João", "12345678900", new DateTime(1980, 5, 20), "joao@medico.com", "drjoao", "1234", "12345", "Cardiologia", "Clínica"));
            pessoas.Add(new Medico("Dra. Ana", "22233344455", new DateTime(1975, 11, 5), "ana@medico.com", "draana", "abcd", "54321", "Pediatria", "Pediatria"));
            pessoas.Add(new Paciente("Maria Silva", "98765432100", new DateTime(1995, 3, 15), "maria@paciente.com", "maria", "1234", "P12345"));
            pessoas.Add(new Paciente("Carlos Pereira", "11122334455", new DateTime(1990, 8, 10), "carlos@paciente.com", "carlos", "abcd", "P54321"));

            Console.WriteLine("=== SISTEMA HOSPITALAR ===");
            Console.WriteLine("Por favor, faça o login.");

            // Laço de login, permitindo múltiplos logins
            while (true)
            {
                Pessoa usuarioLogado = Login();
                if (usuarioLogado != null)
                {
                    if (usuarioLogado is Paciente paciente)
                        MenuPaciente(paciente);
                    else if (usuarioLogado is Medico medico)
                        MenuMedico(medico);
                }
                else
                {
                    Console.WriteLine("Usuário ou senha incorretos.");
                }
            }
        }

        static Pessoa Login()
        {
            Console.Write("Usuário: ");
            string usuario = Console.ReadLine();
            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            return pessoas.FirstOrDefault(p => p.Usuario == usuario && p.Senha == senha);
        }

        static void MenuPaciente(Paciente paciente)
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=== MENU DO PACIENTE ===");
                Console.WriteLine("1. Agendar Consulta");
                Console.WriteLine("2. Ver Receitas");
                Console.WriteLine("3. Ver Consultas Agendadas");
                Console.WriteLine("4. Explicar Funcionalidades");
                Console.WriteLine("5. Ver Dados do Usuário");
                Console.WriteLine("6. Sair para Menu Principal");
                Console.WriteLine("7. Encerrar");
                Console.Write("Escolha uma opção: ");
                Int32.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1:
                        AgendarConsulta(paciente);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("=== SUAS RECEITAS ===");

                        var minhasReceitas = receitas.Where(r => r.Paciente == paciente).ToList();

                        if (!minhasReceitas.Any())
                        {
                            Console.WriteLine("Nenhuma receita disponível.");
                        }
                        else
                        {
                            foreach (var receita in minhasReceitas)
                            {
                                Console.WriteLine($"Médico: Dr(a). {receita.Medico.Nome} | Data: {receita.Data:dd/MM/yyyy}");
                                Console.WriteLine($"Conteúdo: {receita.Conteudo}\n");
                            }
                        }
                        break;
                    case 3:
                        VerConsultasPaciente(paciente);
                        break;
                    case 4:
                        MostrarExplicacoesPaciente();
                        break;
                    case 5:
                        Console.WriteLine("\n=== SEUS DADOS ===");
                        paciente.ExibirDados();
                        break;
                    case 6:
                        Console.WriteLine("Voltando para o menu principal...");
                        return; // Volta para o menu principal
                    case 7:
                        Console.WriteLine("Encerrando o programa...");
                        Environment.Exit(0); // Encerra o programa
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            } while (opcao != 0);
        }

        static void AgendarConsulta(Paciente paciente)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== AGENDAR CONSULTA ===");

                var medicos = pessoas.OfType<Medico>().ToList();
                if (medicos.Count == 0)
                {
                    Console.WriteLine("Nenhum médico cadastrado.");
                    return;
                }

                for (int i = 0; i < medicos.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. Dr(a). {medicos[i].Nome} - {medicos[i].Especialidade}");
                }
                Console.WriteLine("0. Voltar");
                Console.Write("Escolha o médico (número): ");

                if (!Int32.TryParse(Console.ReadLine(), out int escolha) || escolha < 0 || escolha > medicos.Count)
                {
                    Console.WriteLine("Seleção inválida. Pressione ENTER para tentar novamente...");
                    Console.ReadLine();
                    continue;
                }

                if (escolha == 0)
                    return;

                var medico = medicos[escolha - 1];

                while (true)
                {
                    Console.Write("Digite a data e hora (formato yyyy-MM-dd HH:mm) ou digite 0 para voltar: ");
                    string entrada = Console.ReadLine();
                    if (entrada == "0") return;

                    if (!DateTime.TryParse(entrada, out DateTime dataHora))
                    {
                        Console.WriteLine("Formato inválido. Pressione ENTER para tentar novamente...");
                        Console.ReadLine();
                        continue;
                    }

                    consultas.Add(new Consulta(paciente, medico, dataHora));
                    Console.WriteLine($"Consulta agendada com Dr(a). {medico.Nome} em {dataHora}.");
                    return;
                }
            }
        }

        static void VerConsultasPaciente(Paciente paciente)
        {
            Console.Clear();
            Console.WriteLine("=== SUAS CONSULTAS AGENDADAS ===");
            var cns = consultas.Where(c => c.Paciente == paciente).ToList();
            if (!cns.Any())
            {
                Console.WriteLine("Nenhuma consulta agendada.");
                return;
            }
            foreach (var c in cns)
            {
                Console.WriteLine($"Dr(a). {c.Medico.Nome} - {c.DataHora}");
            }
        }

        static void MostrarExplicacoesPaciente()
        {
            Console.WriteLine("\n1. Agendar Consulta: Permite escolher um médico e horário disponíveis.");
            Console.WriteLine("2. Ver Receitas: Mostra as receitas médicas já emitidas para você.");
            Console.WriteLine("3. Ver Consultas Agendadas: Lista suas consultas marcadas.");
            Console.WriteLine("4. Explicar Funcionalidades: Exibe o que cada função faz.");
            Console.WriteLine("5. Ver Dados do Usuário: Mostra seus dados pessoais.");
        }

        static void MenuMedico(Medico medico)
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=== MENU DO MÉDICO ===");
                Console.WriteLine("1. Consultar Pacientes");
                Console.WriteLine("2. Ver Consultas Agendadas");
                Console.WriteLine("3. Adicionar Receita");
                Console.WriteLine("4. Explicar Funcionalidades");
                Console.WriteLine("5. Ver Dados do Usuário");
                Console.WriteLine("6. Voltar ao Menu Principal");
                Console.WriteLine("0. Encerrar");
                Console.Write("Escolha uma opção: ");
                Int32.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1:
                        ConsultarPacientes(medico);
                        break;
                    case 2:
                        VerConsultasMedico(medico);
                        break;
                    case 3:
                        AdicionarReceita(medico);
                        break;
                    case 4:
                        MostrarExplicacoesMedico();
                        break;
                    case 5:
                        Console.WriteLine("\n=== SEUS DADOS ===");
                        medico.ExibirDados();
                        break;
                    case 6:
                        return;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            } while (opcao != 0);
        }

        static void ConsultarPacientes(Medico medico)
        {
            Console.Clear();
            Console.WriteLine("=== PACIENTES QUE AGENDARAM CONSULTAS ===");

            var consultasAgendadas = consultas.Where(c => c.Medico == medico).ToList();

            var pacientesUnicos = consultasAgendadas
                .Select(c => c.Paciente)
                .Distinct()
                .ToList();

            if (!pacientesUnicos.Any())
            {
                Console.WriteLine("Nenhum paciente agendado.");
                return;
            }

            foreach (var paciente in pacientesUnicos)
            {
                Console.WriteLine($"\nPaciente: {paciente.Nome}");
                paciente.ExibirDados();
            }
        }

        static void AdicionarReceita(Medico medico)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ADICIONAR RECEITA ===");

                var pacientes = consultas
                    .Where(c => c.Medico == medico)
                    .Select(c => c.Paciente)
                    .Distinct()
                    .ToList();

                if (!pacientes.Any())
                {
                    Console.WriteLine("Você ainda não possui pacientes com consultas marcadas.");
                    return;
                }

                for (int i = 0; i < pacientes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {pacientes[i].Nome}");
                }
                Console.WriteLine("0. Voltar");
                Console.Write("Escolha o paciente (número): ");
                if (!Int32.TryParse(Console.ReadLine(), out int escolha) || escolha < 0 || escolha > pacientes.Count)
                {
                    Console.WriteLine("Opção inválida. Pressione ENTER para tentar novamente...");
                    Console.ReadLine();
                    continue;
                }

                if (escolha == 0)
                    return;

                var paciente = pacientes[escolha - 1];

                Console.WriteLine("Digite a receita médica ou digite 0 para voltar:");
                string conteudo = Console.ReadLine();

                if (conteudo == "0")
                    return;

                receitas.Add(new Receita(paciente, medico, conteudo));
                Console.WriteLine("Receita adicionada com sucesso!");
                return;
            }
        }

        static void VerConsultasMedico(Medico medico)
        {
            Console.Clear();
            Console.WriteLine("=== SUAS CONSULTAS AGENDADAS ===");
            var cns = consultas.Where(c => c.Medico == medico).ToList();
            if (!cns.Any())
            {
                Console.WriteLine("Nenhuma consulta agendada.");
                return;
            }
            foreach (var c in cns)
            {
                Console.WriteLine($"Paciente: {c.Paciente.Nome} - {c.DataHora}");
            }
        }

        static void MostrarExplicacoesMedico()
        {
            Console.WriteLine("\n1. Consultar Pacientes: Exibe os pacientes que agendaram consultas com você.");
            Console.WriteLine("2. Ver Consultas Agendadas: Lista suas consultas marcadas.");
            Console.WriteLine("3. Explicar Funcionalidades: Exibe o que cada função faz.");
            Console.WriteLine("4. Ver Dados do Usuário: Mostra seus dados pessoais.");
        }
    }
}
