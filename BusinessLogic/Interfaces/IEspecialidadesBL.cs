using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IEspecialidadesBL
    {
        Task<List<Especialidad>> RecEspecialidadesxUsuarioAsync(int tipoAgenda, int usuarioId, int sucursal);
    }
}
