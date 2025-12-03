using Entities.Models;
using System.Collections.Generic;

namespace BussinessLogic.Interfaces
{
    public interface IExpedienteBL
    {
        List<Expediente> RecExpediente();
        Expediente? RecExpedienteXId(int numExpediente);
        bool InsExpediente(Expediente expediente);
        bool ModExpediente(Expediente expediente);
        bool DelExpediente(int numExpediente);
    }
}
