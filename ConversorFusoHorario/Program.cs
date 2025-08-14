using ConversorFusoHorario;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

// See https://aka.ms/new-console-template for more information
var hoje = DateTimeOffset.Now;
var em = hoje.ToOffset(TimeSpan.FromHours(-8));


Console.WriteLine(hoje);
Console.WriteLine(em);

// mm/dd/yyyy
// hh:mm:ss 
// AM or PM
// Titulo
// Opcoes de TZ - BR, JP, GB, US


List<AgendaEntrada> tarefas = new List<AgendaEntrada>();

Console.WriteLine("Bem vindo a agenda, vamos começar?");

try
{
    Console.WriteLine("Qual a fuso-horário você deseja utilizar?");
    Console.WriteLine("1 - Brasil (DF)"); //E. South America Standard Time	(UTC-03:00)
    Console.WriteLine("2 - China (Hong Kong)"); //China Standard Time	(UTC+08:00
    Console.WriteLine("3 - Suécia (Stockholm)"); // W. Europe Standard Time	(UTC+01:00)
    Console.WriteLine("0 - UTC (Default)"); //UTC
    int fuso = 0;
    int fusoInput = Convert.ToInt32(Console.ReadLine());
    if (fusoInput > 1 && fusoInput < 3)
    { 
        fuso = fusoInput;
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

while (true)
{
    int opcao = 0;
    Console.WriteLine("Digite 1 para adicinar uma tarefa...");
    Console.WriteLine("Digite 2 para verificar tarefas...");
    Console.WriteLine("Digite 3 para sair...");
    try
    {
        opcao = int.Parse(Console.ReadLine());
        switch (opcao)
        {
            case 1:
                bool valido = false;

                Console.WriteLine("Qual seria o titulo do compromisso?");
                string titulo = Console.ReadLine();
                
                DateTime dataFinal = DateTime.Now;

                while (valido == false)
                {
                    Console.WriteLine("Digite a data do compromisso (MM/dd/yyyy)");
                    string data = Console.ReadLine();
                    Console.Write("Digite o horário do compromisso (hh:mm:ss)");
                    string horario = Console.ReadLine();
                    Console.WriteLine("Seria da manhã ou da tarde? (AM - Manhã | PM - Tarde/Noite)");
                    string periodo = Console.ReadLine();

                    string dataCompleta = data + " " + horario + " " + periodo;
                    
                    
                    try
                    {
                        if (DateTime.TryParse(dataCompleta, out dataFinal))
                        {
                            Console.WriteLine("Data valida...");
                            valido = true;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Data no formato inválido");
                    }
                }
                
                
                AgendaEntrada tarefa = new AgendaEntrada(dataFinal, titulo);
                tarefas.Add(tarefa); 
                Console.WriteLine($"Tarefa {titulo} adicionada com sucesso para {dataFinal.Date}!");
                break;
            
            case 2:
                Console.WriteLine("Verificando tarefas");
                break;
            case 0:
                Environment.Exit(0);
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Digite uma opção válida");
    }
}

