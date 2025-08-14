using ConversorFusoHorario;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

// See https://aka.ms/new-console-template for more information
var hoje = DateTime.Now;
Console.WriteLine(hoje);
// mm/dd/yyyy
// hh:mm:ss 
// AM or PM
// Titulo
// Opcoes de TZ - BR, JP, GB, US


try
{
    int opcao = 0;
    Console.WriteLine("Bem vindo a agenda, vamos começar?");
    Console.WriteLine("Digite 1 para adicinar uma tarefa...");
    Console.WriteLine("Digite 2 para verificar tarefas...");
    Console.WriteLine("Digite 3 para sair...");
}
    

    try
    {
        switch (opcao)
        {
            case 1:


                Console.WriteLine("Digite a data da tarefa (MM/dd/yyyy)");
                string data = Console.ReadLine();
                Console.Write("Digite o horário da tarefa (hh:mm:ss)");
                string horario = Console.ReadLine();
                Console.WriteLine("Seria da manhã ou da tarde? (AM - Manhã | PM - Tarde/Noite)");
                string periodo = Console.ReadLine();

                string dataCompleta = data + " " + horario + " " + periodo;
                DateTime dataFinal;
                try
                {
                    if (DateTime.TryParse(dataCompleta, out dataFinal))
                    {
                        Console.WriteLine(dataFinal);
                    }
                }
                catch
                {
                    Console.WriteLine("Data no formato inválido");
                }
               

                break;
            case 2:
                Console.WriteLine("Verificando tarefas");
                break;
            case 0:
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Digite uma opção válida");



    }
