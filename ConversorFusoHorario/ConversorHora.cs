using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorFusoHorario
{
    public class ConversorHora : IConversorHora
    {
        public DateTime ConverterParaFusoHorario(DateTime dataHora, string idFusoDestino)
        {
            try
            {
                //fh de destino
                TimeZoneInfo tzDestino = TimeZoneInfo.FindSystemTimeZoneById(idFusoDestino);

                return TimeZoneInfo.ConvertTimeFromUtc(dataHora, tzDestino);
            }
            catch (TimeZoneNotFoundException)
            {
                throw new Exception("Fuso Horario nao encontrado");
            }
        }

        public string ObterFusoHorarioDaData(string dataHoraStr)
        {
            //Não entendemos muito bem o proposito desse método.
            throw new NotImplementedException();
        }
    }
}
