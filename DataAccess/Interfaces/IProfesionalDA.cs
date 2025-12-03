using Entities.Models;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IProfesionalDA
    {
        List<Profesional> RecProfesionales();
        Profesional? RecProfesionalXCod(string codProf);
        bool InsProfesional(Profesional prof);
        bool ModProfesional(Profesional prof);
        bool DelProfesional(string codProf);
    }
}
