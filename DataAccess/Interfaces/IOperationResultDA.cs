using Entities.Models;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IOperationResultDA
    {
        IEnumerable<OperationResult> ObtenerResultados();
        OperationResult ObtenerResultadoPorCodigo(int code);
        bool InsertarResultado(OperationResult result);
        bool EliminarResultado(int code);
    }
}
