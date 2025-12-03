using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IPacienteDA
    {

        List<Expediente> GetRecordByName(string primerNom, string segundoNom, string primerAp, string segundoAp);

        List<Expediente> GetRecordByIdentification(string pidentificacion, string pcod_tipdoc);

        List<string> InsertNewRecord(Expediente pExpediente);
    }
}
