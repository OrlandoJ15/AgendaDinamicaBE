using System.Collections.Generic;
using BussinessLogic.Interfaces;
using DataAccess.Interfaces;
using Entities.Models;

namespace BussinessLogic.Implementation
{
    public class OperationResultBL : IOperationResultBL
    {
        private readonly IOperationResultDA _operationDA;

        public OperationResultBL(IOperationResultDA operationDA)
        {
            _operationDA = operationDA;
        }

        public IEnumerable<OperationResult> ObtenerResultados() => _operationDA.ObtenerResultados();

        public OperationResult ObtenerResultadoPorCodigo(int code) => _operationDA.ObtenerResultadoPorCodigo(code);

        public bool InsertarResultado(OperationResult result) => _operationDA.InsertarResultado(result);

        public bool EliminarResultado(int code) => _operationDA.EliminarResultado(code);
    }
}
