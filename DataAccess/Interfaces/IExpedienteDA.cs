using Entities.Models;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IExpedienteDA
    {
        List<Expediente> RecExpediente();
        Expediente? RecExpedienteXId(int numExpediente);
        bool InsExpediente(Expediente expediente);
        bool ModExpediente(Expediente expediente);
        bool DelExpediente(int numExpediente);
    }
}
