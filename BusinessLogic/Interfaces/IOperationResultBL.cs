using Entities.Models;
using System.Collections.Generic;

namespace BussinessLogic.Interfaces
{
    public interface IOperationResultBL
    {
        IEnumerable<OperationResult> ObtenerResultados();
        OperationResult ObtenerResultadoPorCodigo(int code);
        bool InsertarResultado(OperationResult result);
        bool EliminarResultado(int code);
    }
}
