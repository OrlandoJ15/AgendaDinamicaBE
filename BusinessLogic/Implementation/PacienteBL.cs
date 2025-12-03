using CommonMethods;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Implementation
{
    public class PacienteBL: IPacienteBL
    {
        private readonly IPacienteDA _pacienteAD;
        public readonly Exceptions _exceptions;

        public PacienteBL(IPacienteDA pacienteAD, Exceptions exceptions)
        {
            _pacienteAD = pacienteAD;
            _exceptions = exceptions;
        }

        // =======================================================
        // MÉTODOS PÚBLICOS DE NEGOCIO
        // =======================================================

        public List<Expediente> GetRecordByName(string primerNom, string segundoNom, string primerAp, string segundoAp) {
            return _exceptions.EjecutarProcConEntidad(() => _pacienteAD.GetRecordByName(primerNom, segundoNom, primerAp, segundoAp));
        }

        public List<Expediente> GetRecordByIdentification(string pidentificacion, string pcod_tipdoc)
        {
            return _exceptions.EjecutarProcConEntidad(() => _pacienteAD.GetRecordByIdentification(pidentificacion, pcod_tipdoc));
        }

        public List<string> InsertNewRecord(Expediente pExpediente) 
        {
            return _exceptions.EjecutarProcConEntidad(() => _pacienteAD.InsertNewRecord(pExpediente));
        }
    }
}
