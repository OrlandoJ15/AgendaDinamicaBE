using Entities.Models;
using System.Collections.Generic;

namespace BussinessLogic.Interfaces
{
    public interface IParamEnvioCorreoBL
    {
        List<ParamEnvioCorreo> RecParamsEnvioCorreo();
        ParamEnvioCorreo RecParamEnvioCorreoXId(string compania);
        void InsParamEnvioCorreo(ParamEnvioCorreo param);
        void ModParamEnvioCorreo(ParamEnvioCorreo param);
        void DelParamEnvioCorreo(string compania);
    }
}
