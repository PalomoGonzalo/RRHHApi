using RRHH.Core.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRHH.Core.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<IEnumerable<UsuarioDTO>> ObtenerUsuarios();

    }
}
