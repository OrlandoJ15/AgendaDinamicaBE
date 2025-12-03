using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ParamEnvioCorreo
    {
        public string COMPAÑIA { get; set; }
        public string DIRECCION_ENVIO { get; set; }
        public string DESTINO { get; set; }
        public string ASUNTO { get; set; }
        public string MENSAJE { get; set; }
        public string DIRECTORIO { get; set; }
        public string ES_HTML { get; set; }
        public string MODULO { get; set; }
    }
}
