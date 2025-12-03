using BusinessLogic.Interfaces;
using CommonMethods;
using DataAccess.Implementation;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace BusinessLogic.Implementation
{
    public class AgendaBL : IAgendaBL
    {
        private readonly IAgendaDA _agendaAD;
        public readonly Exceptions _exceptions;

        public AgendaBL(IAgendaDA agendaAD, Exceptions exceptions)
        {
            _agendaAD = agendaAD;
            _exceptions = exceptions;
        }

        // =======================================================
        // MÉTODOS PÚBLICOS DE NEGOCIO
        // =======================================================

        public List<Agenda> RecAgenda()
        {
            return _exceptions.EjecutarProcConEntidad(() => _agendaAD.RecAgenda());
        }

        public Agenda? RecAgendaXId(int numAgenda)
        {
            return _exceptions.EjecutarProcConEntidad(() => _agendaAD.RecAgendaXId(numAgenda));
        }

        public bool InsAgenda(Agenda agenda)
        {
            return _exceptions.EjecutarProcSinEntidad(() => _agendaAD.InsAgenda(agenda));
        }

        public bool ModAgenda(Agenda agenda)
        {
            return _exceptions.EjecutarProcSinEntidad(() => _agendaAD.ModAgenda(agenda));
        }

        public bool DelAgenda(int numAgenda)
        {
            return _exceptions.EjecutarProcSinEntidad(() => _agendaAD.DelAgenda(numAgenda));
        }
    }
}
