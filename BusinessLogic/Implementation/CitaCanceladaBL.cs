using DataAccess.Implementation;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using CommonMethods;
using BusinessLogic.Interfaces;

namespace BusinessLogic.Implementation
{
    public class CitaCanceladaBL : ICitaCanceladaBL
    {
        private readonly ICitaCanceladaDA gObjCitaCanceladaAD;
        public Exceptions gObjExceptions = new Exceptions();

        public CitaCanceladaBL(IConfiguration configuration)
        {
            gObjCitaCanceladaAD = new CitaCanceladaDA(configuration);
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

        public List<CitaCancelada> RecCitasCanceladas()
        {
            return EjecutarProcConEntidad(() => gObjCitaCanceladaAD.RecCitasCanceladas());
        }

        public CitaCancelada? RecCitaCanceladaXId(int numCitaCancelada)
        {
            return EjecutarProcConEntidad(() => gObjCitaCanceladaAD.RecCitaCanceladaXId(numCitaCancelada));
        }

        public bool InsCitaCancelada(CitaCancelada cita)
        {
            return EjecutarProcSinEntidad(() => gObjCitaCanceladaAD.InsCitaCancelada(cita));
        }

        public bool ModCitaCancelada(CitaCancelada cita)
        {
            return EjecutarProcSinEntidad(() => gObjCitaCanceladaAD.ModCitaCancelada(cita));
        }

        public bool DelCitaCancelada(int numCitaCancelada)
        {
            return EjecutarProcSinEntidad(() => gObjCitaCanceladaAD.DelCitaCancelada(numCitaCancelada));
        }
    }
}
