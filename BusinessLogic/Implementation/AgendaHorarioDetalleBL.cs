using DataAccess.Implementation;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using CommonMethods;
using BussinessLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace BussinessLogic.Implementation
{
    public class AgendaHorarioDetalleBL : IAgendaHorarioDetalleBL
    {
        private readonly IAgendaHorarioDetalleAD gObjAgAgendaHorarioDetalleAD;
        public Exceptions gObjExceptions = new Exceptions();

        public AgendaHorarioDetalleBL(IConfiguration configuration)
        {
            gObjAgAgendaHorarioDetalleAD = new AgendaHorarioDetalleDA(configuration);
        }

        // =======================================================
        // MÉTODOS PRIVADOS DE EJECUCIÓN CENTRALIZADA
        // =======================================================

        private T EjecutarProcConEntidad<T>(Func<T> funcion)
        {
            try
            {
                return funcion();
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }
        }

        private bool EjecutarProcSinEntidad(Action accion)
        {
            try
            {
                accion();
                return true;
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }
        }

        // =======================================================
        // MÉTODOS PÚBLICOS DE NEGOCIO
        // =======================================================

        public List<AgendaHorarioDetalle> RecAgendaHorarioDetalles()
        {
            return EjecutarProcConEntidad(() => gObjAgAgendaHorarioDetalleAD.RecAgendaHorarioDetalles());
        }

        public AgendaHorarioDetalle? RecAgendaHorarioDetalleXId(int idDetalle)
        {
            return EjecutarProcConEntidad(() => gObjAgAgendaHorarioDetalleAD.RecAgendaHorarioDetalleXId(idDetalle));
        }

        public bool InsAgendaHorarioDetalle(AgendaHorarioDetalle detalle)
        {
            return EjecutarProcSinEntidad(() => gObjAgAgendaHorarioDetalleAD.InsAgendaHorarioDetalle(detalle));
        }

        public bool ModAgendaHorarioDetalle(AgendaHorarioDetalle detalle)
        {
            return EjecutarProcSinEntidad(() => gObjAgAgendaHorarioDetalleAD.ModAgendaHorarioDetalle(detalle));
        }

        public bool DelAgendaHorarioDetalle(int idDetalle)
        {
            return EjecutarProcSinEntidad(() => gObjAgAgendaHorarioDetalleAD.DelAgendaHorarioDetalle(idDetalle));
        }
    }
}
