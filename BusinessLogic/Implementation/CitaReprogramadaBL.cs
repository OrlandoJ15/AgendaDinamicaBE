using DataAccess.Implementation;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using CommonMethods;
using BussinessLogic.Interfaces;

namespace BusinessLogic.Implementation
{
    public class CitaReprogramadaBL : ICitaReprogramadaBL
    {
        private readonly ICitaReprogramadaDA gObjCitaReprogramadaDA;
        public Exceptions gObjExceptions = new Exceptions();

        public CitaReprogramadaBL(IConfiguration configuration)
        {
            gObjCitaReprogramadaDA = new CitaReprogramadaDA(configuration);
        }

        private T EjecutarProcConEntidad<T>(Func<T> funcion)
        {
            try { return funcion(); }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }
        }

        private bool EjecutarProcSinEntidad(Func<bool> funcion)
        {
            try { return funcion(); }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }
        }

        public List<CitaReprogramada> RecCitasReprogramadas()
            => EjecutarProcConEntidad(() => gObjCitaReprogramadaDA.RecCitasReprogramadas());

        public CitaReprogramada? RecCitaReprogramadaXId(int numCitaReprogramada)
            => EjecutarProcConEntidad(() => gObjCitaReprogramadaDA.RecCitaReprogramadaXId(numCitaReprogramada));

        public bool InsCitaReprogramada(CitaReprogramada cita)
            => EjecutarProcSinEntidad(() => gObjCitaReprogramadaDA.InsCitaReprogramada(cita));

        public bool ModCitaReprogramada(CitaReprogramada cita)
            => EjecutarProcSinEntidad(() => gObjCitaReprogramadaDA.ModCitaReprogramada(cita));

        public bool DelCitaReprogramada(int numCitaReprogramada)
            => EjecutarProcSinEntidad(() => gObjCitaReprogramadaDA.DelCitaReprogramada(numCitaReprogramada));
    }
}
