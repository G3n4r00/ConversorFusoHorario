using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorFusoHorario
{
    public class AgendaEntrada(DateTime DataHora, String Titulo): IAgendaEntrada
    {
        /// <summary>
        /// Data e hora do compromisso.
        /// </summary>
        public DateTime DataHora { get; set; }

        /// <summary>
        /// Título do compromisso.
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Imprime a entrada completa (data/hora e título), convertendo a data/hora para o timezone informado, se fornecido.
        /// </summary>
        /// <param name="idFusoDestino">ID do fuso horário de destino (opcional).</param>
        public void Imprimir(string? idFusoDestino = null)
        {
            
        }

        /// <summary>
        /// Imprime apenas a hora do compromisso, convertendo para o timezone informado, se fornecido.
        /// </summary>
        /// <param name="idFusoDestino">ID do fuso horário de destino (opcional).</param>
        public void ImprimirHora(string? idFusoDestino = null)
        {

        }

        /// <summary>
        /// Imprime apenas o dia do compromisso, convertendo para o timezone informado, se fornecido.
        /// </summary>
        /// <param name="idFusoDestino">ID do fuso horário de destino (opcional).</param>
        public void ImprimirDia(string? idFusoDestino = null)
        {

        }

        /// <summary>
        /// Imprime o dia da semana do compromisso, convertendo para o timezone informado, se fornecido.
        /// </summary>
        /// <param name="idFusoDestino">ID do fuso horário de destino (opcional).</param>
        public void ImprimirDiaSemana(string? idFusoDestino = null)
        {

        }
    }
}
