using ConversorFusoHorario;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;


//Objetivo:

//•Ler uma entrada de agenda com data/hora e um timezone.
//•Exibir compromissos para o dia de acordo com um timezone informado.
//•Exibir compromissos para uma data de acordo com o timezone informado


// See https://aka.ms/new-console-template for more information
var hoje = DateTimeOffset.UtcNow;
var em = hoje.ToOffset(TimeSpan.FromHours(-8));

var agora = DateTimeOffset.Now;
Console.WriteLine(agora);


Console.WriteLine(hoje);
Console.WriteLine(em);

// mm/dd/yyyy
// hh:mm:ss 
// AM or PM
// Titulo
// Opcoes de TZ - BR, JP, GB, US


List<AgendaEntrada> tarefas = new List<AgendaEntrada>();
ConversorHora conversor = new ConversorHora();  

Console.WriteLine("Bem vindo a agenda, vamos começar?");


while (true)
{
    String opcao;
    Console.WriteLine("Digite 1 para adicinar uma tarefa...");
    Console.WriteLine("Digite 2 para verificar tarefas de hoje...");
    Console.WriteLine("Digite 3 para verificar tarefas para dia exato...");
    Console.WriteLine("Digite 4 para visualizar todas as tarefas...");
    Console.WriteLine("Digite 5 para sair...");
    try
    {
        opcao = Console.ReadLine();
        switch (opcao)
        {
            case "1":
                bool valido = false;
                Console.WriteLine("Qual seria o titulo do compromisso?");
                string titulo = Console.ReadLine();
                string fusoHorario = "";
                DateTime dataFinal = DateTime.Now;
                TimeSpan offset = TimeSpan.Zero;

                while (valido == false)
                {
                    Console.WriteLine("Digite a data do compromisso (dd/MM/aaaa)");
                    string data = Console.ReadLine();

                    Console.Write("Digite o horário do compromisso (HH:mm)");
                    string horario = Console.ReadLine();

                    string dataCompleta = data + " " + horario;
                    
                    if (DateTime.TryParseExact(dataCompleta, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFinal))
                    {
                        valido = true;
                    }
                    else
                    {
                        Console.WriteLine("Data ou horário inválidos. Tente novamente.");
                    }
                }


                valido = false;
                string efuso = "";  

                while (valido == false)
                {
                    Console.WriteLine("Escolha o fuso horário para visualização:");
                    Console.WriteLine("1 - BR (UTC-3)");
                    Console.WriteLine("2 - JP (UTC+9)");
                    Console.WriteLine("3 - GB (UTC)");
                    Console.WriteLine("4 - US (UTC-8)");
                    efuso = Console.ReadLine();
                    switch (efuso)
                    {
                        case "1":
                            offset = TimeSpan.FromHours(-3); 
                            valido = true;
                            efuso = "BR";
                            break;
                        case "2":
                            offset = TimeSpan.FromHours(+9);
                            valido = true;
                            efuso = "JP";
                            break;
                        case "3":
                            valido = true;
                            efuso = "GB";
                            break;
                        case "4":
                            offset = TimeSpan.FromHours(-8); 
                            valido = true;
                            efuso = "US";
                            break;
                        default:
                            Console.WriteLine("Fuso horário inválido. Tente novamente.");
                            break;
                    }
                }


                DateTime dataNoFusoLocal = dataFinal; // Data que o usuário digitou
                DateTime dataUTC = dataNoFusoLocal.Subtract(offset); 
                dataUTC = DateTime.SpecifyKind(dataUTC, DateTimeKind.Utc);

                AgendaEntrada tarefa = new AgendaEntrada(dataUTC, titulo);


                tarefas.Add(tarefa);

                Console.WriteLine($"Compromisso '{titulo}' adicionado com sucesso para {dataNoFusoLocal:dd/MM/yyyy HH:mm}, fuso: {efuso}");
                Console.WriteLine($"(Armazenado em UTC: {dataUTC:dd/MM/yyyy HH:mm})");

                break;
            
            case "2":
                Console.WriteLine("Verificando as tarefas.....");
                Console.WriteLine("Escolha o fuso horário para visualização:");
                Console.WriteLine("1 - BR (UTC-3)");
                Console.WriteLine("2 - JP (UTC+9)");
                Console.WriteLine("3 - GB (UTC)");
                Console.WriteLine("4 - US (UTC-8)");
                string escolha = Console.ReadLine();

                String tz;
                switch (escolha)
                {
                    case "1": tz = "E. South America Standard Time"; break;
                    case "2": tz = "Tokyo Standard Time"; break;
                    case "3": tz = "GMT Standard Time"; break;
                    case "4": tz = "Pacific Standard Time"; break;
                    default:
                        Console.WriteLine("Fuso horário inválido. Usando UTC.");
                        tz = "UTC";
                        break;
                }

                DateTime hojeNoFuso = conversor.ConverterParaFusoHorario(DateTime.UtcNow, tz);

                // Filtra compromissos que caem no dia de hoje no fuso selecionado
                var compromissosHoje = tarefas.Where(e => conversor.ConverterParaFusoHorario(e.DataHora, tz).Date == hojeNoFuso);

                Console.WriteLine($"\nCompromissos para hoje ({hojeNoFuso:dd/MM/yyyy}) no fuso selecionado:");
                foreach (var entrada in compromissosHoje)
                {
                    entrada.Imprimir(); // Mostra no fuso selecionado
                }
                break;

            case "3":
                Console.WriteLine("Digite a data que deseja verificar (dd/MM/aaaa):");
                string dataConsulta = Console.ReadLine();
                DateTime dataConsultaParsed;
                if (!DateTime.TryParseExact(dataConsulta, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataConsultaParsed))
                {
                    Console.WriteLine("Data inválida. Tente novamente.");
                    break;
                }
                Console.WriteLine("Escolha o fuso horário para visualização:");
                Console.WriteLine("1 - BR (UTC-3)");
                Console.WriteLine("2 - JP (UTC+9)");
                Console.WriteLine("3 - GB (UTC)");
                Console.WriteLine("4 - US (UTC-8)");
                string escolhaData = Console.ReadLine();
                String tzData;
                switch (escolhaData)
                {
                    case "1": tzData = "E. South America Standard Time"; break;
                    case "2": tzData = "Tokyo Standard Time"; break;
                    case "3": tzData = "GMT Standard Time"; break;
                    case "4": tzData = "Pacific Standard Time"; break;
                    default:
                        Console.WriteLine("Fuso horário inválido. Usando UTC.");
                        tzData = "UTC";
                        break;
                }
                DateTime dataNoFuso = conversor.ConverterParaFusoHorario(dataConsultaParsed, tzData);
                var compromissosNaData = tarefas.Where(e => conversor.ConverterParaFusoHorario(e.DataHora, tzData).Date == dataNoFuso.Date);
                Console.WriteLine($"\nCompromissos para {dataNoFuso:dd/MM/yyyy} no fuso selecionado:");
                foreach (var entrada in compromissosNaData)
                {
                    entrada.Imprimir(); // Mostra no fuso selecionado
                }
                break;

            case "4":
                Console.WriteLine("Escolha o fuso horário para visualização:");
                Console.WriteLine("1 - BR (UTC-3)");
                Console.WriteLine("2 - JP (UTC+9)");
                Console.WriteLine("3 - GB (UTC)");
                Console.WriteLine("4 - US (UTC-8)");
                string escolhafuso = Console.ReadLine();
                String tzAll;
                switch (escolhafuso)
                {
                    case "1": tzAll = "E. South America Standard Time"; break;
                    case "2": tzAll = "Tokyo Standard Time"; break;
                    case "3": tzAll = "GMT Standard Time"; break;
                    case "4": tzAll = "Pacific Standard Time"; break;
                    default:
                        Console.WriteLine("Fuso horário inválido. Usando UTC.");
                        tzAll = "UTC";
                        break;
                }
                Console.WriteLine("\n=== LISTA DE COMPROMISSOS ===");
                if (tarefas.Count == 0)
                {
                    Console.WriteLine("Nenhum compromisso encontrado.");
                }
                else
                {
                    foreach (var entrada in tarefas)
                    {
                        entrada.Imprimir(tzAll);
                    }
                }
                Console.WriteLine("===========================\n");
                break;

            case "5":
                Environment.Exit(0);
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

