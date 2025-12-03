using Entities.Models;

namespace DataAccess.Interfaces
{
    public interface ITExpedienteAD
    {
        List<Expediente> RecExpedientes();
        Expediente? RecExpedienteXId(int numExpediente);
        bool InsExpediente(Expediente expediente);
        bool ModExpediente(Expediente expediente);
        bool DelExpediente(int numExpediente);
    }
}
