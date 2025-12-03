using DataAccess.Implementation;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using CommonMethods;
using BussinessLogic.Interfaces;

namespace BusinessLogic.Implementation
{
    public class CitaProcedimientoBL : ICitaProcedimientoBL
    {
        private readonly ICitaProcedimientoDA gObjCitaProcedimientoAD;
        public Exceptions gObjExceptions = new Exceptions();

        public CitaProcedimientoBL(IConfiguration configuration)
        {
            gObjCitaProcedimientoAD = new CitaProcedimientoDA(configuration);
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

        private bool EjecutarProcSinEntidad(Func<bool> funcion)
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

        // =======================================================
        // MÉTODOS PÚBLICOS DE NEGOCIO
        // =======================================================

        public List<CitaProcedimiento> RecCitaProcedimientos()
        {
            return EjecutarProcConEntidad(() => gObjCitaProcedimientoAD.RecCitaProcedimientos());
        }

        public CitaProcedimiento? RecCitaProcedimientoXId(int numCita, string codArticulo)
        {
            return EjecutarProcConEntidad(() => gObjCitaProcedimientoAD.RecCitaProcedimientoXId(numCita, codArticulo));
        }

        public bool InsCitaProcedimiento(CitaProcedimiento citaProc)
        {
            return EjecutarProcSinEntidad(() => gObjCitaProcedimientoAD.InsCitaProcedimiento(citaProc));
        }

        public bool ModCitaProcedimiento(CitaProcedimiento citaProc)
        {
            return EjecutarProcSinEntidad(() => gObjCitaProcedimientoAD.ModCitaProcedimiento(citaProc));
        }

        public bool DelCitaProcedimiento(int numCita, string codArticulo)
        {
            return EjecutarProcSinEntidad(() => gObjCitaProcedimientoAD.DelCitaProcedimiento(numCita, codArticulo));
        }
    }
}
