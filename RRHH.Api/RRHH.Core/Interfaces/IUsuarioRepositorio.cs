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
        String GenerarToken(UserLoginDTO seguridad);
        Task<UsuarioDTO> ObtenerUsuarioPorId(int id);

    }
}
