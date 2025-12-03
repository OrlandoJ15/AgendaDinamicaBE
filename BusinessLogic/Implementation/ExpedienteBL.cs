using Entities.Models;
using BussinessLogic.Interfaces;
using DataAccess.Interfaces;
using System.Collections.Generic;

namespace BussinessLogic.Implementation
{
    public class ExpedienteBL : IExpedienteBL
    {
        private readonly IExpedienteDA _expedienteAD;

        public ExpedienteBL(IExpedienteDA expedienteAD)
        {
            _expedienteAD = expedienteAD;
        }

        public List<Expediente> RecExpediente() => _expedienteAD.RecExpediente();
        public Expediente? RecExpedienteXId(int numExpediente) => _expedienteAD.RecExpedienteXId(numExpediente);
        public bool InsExpediente(Expediente expediente) => _expedienteAD.InsExpediente(expediente);
        public bool ModExpediente(Expediente expediente) => _expedienteAD.ModExpediente(expediente);
        public bool DelExpediente(int numExpediente) => _expedienteAD.DelExpediente(numExpediente);
    }
}
