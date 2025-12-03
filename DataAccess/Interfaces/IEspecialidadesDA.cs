using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IEspecialidadesDA
    {
        Task<List<Especialidad>> RecEspecialidadesxUsuarioAsync(int tipoAgenda, int usuarioId, int sucursal);
    }
}
