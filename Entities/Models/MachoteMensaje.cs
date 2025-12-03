using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class MachoteMensaje
    {
        public int Num_Id { get; set; }
        public string Titulo { get; set; }
        public string Des_Mensaje_Correo { get; set; }
        public string Des_Mensaje_SMS { get; set; }
        public string Ind_Activo { get; set; }
        public string Ind_AplicaIngles { get; set; }
        public string Correo_ConfirmacionNuevaCita { get; set; }
    }
}
