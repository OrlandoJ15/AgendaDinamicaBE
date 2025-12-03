using BussinessLogic.Interfaces;
using DataAccess.Interfaces;
using Entities.Models;
using CommonMethods;

namespace BusinessLogic.Implementation
{
    public class ProfesionalBL : IProfesionalBL
    {
        private readonly IProfesionalDA _profAD;
        private readonly Exceptions _exceptions = new Exceptions();

        public ProfesionalBL(IProfesionalDA profAD)
        {
            _profAD = profAD;
        }

        private T EjecutarProcConEntidad<T>(Func<T> funcion)
        {
            try
            {
                return funcion();
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
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
                _exceptions.LogError(ex);
                throw;
            }
        }

        public List<Profesional> RecProfesionales() => EjecutarProcConEntidad(() => _profAD.RecProfesionales());
        public Profesional? RecProfesionalXCod(string codProf) => EjecutarProcConEntidad(() => _profAD.RecProfesionalXCod(codProf));
        public bool InsProfesional(Profesional prof) => EjecutarProcSinEntidad(() => _profAD.InsProfesional(prof));
        public bool ModProfesional(Profesional prof) => EjecutarProcSinEntidad(() => _profAD.ModProfesional(prof));
        public bool DelProfesional(string codProf) => EjecutarProcSinEntidad(() => _profAD.DelProfesional(codProf));
    }
}
