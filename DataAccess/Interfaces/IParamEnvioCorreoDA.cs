using Entities.Models;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IParamEnvioCorreoDA
    {
        List<ParamEnvioCorreo> RecParamsEnvioCorreo();
        ParamEnvioCorreo RecParamEnvioCorreoXId(string compania);
        void InsParamEnvioCorreo(ParamEnvioCorreo param);
        void ModParamEnvioCorreo(ParamEnvioCorreo param);
        void DelParamEnvioCorreo(string compania);
    }
}
