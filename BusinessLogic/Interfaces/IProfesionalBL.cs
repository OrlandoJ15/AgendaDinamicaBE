using Entities.Models;
using System.Collections.Generic;

namespace BussinessLogic.Interfaces
{
    public interface IProfesionalBL
    {
        List<Profesional> RecProfesionales();
        Profesional? RecProfesionalXCod(string codProf);
        bool InsProfesional(Profesional prof);
        bool ModProfesional(Profesional prof);
        bool DelProfesional(string codProf);
    }
}
