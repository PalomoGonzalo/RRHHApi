using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RRHH.Core.DTOS;

namespace RRHH.Core.Interfaces
{
    public interface IUsuarioContraseñaRepositorio
    {
         Task<UserLoginDTO> ObtenerUsuarioContraseña (UserLoginDTO user);
         Task<UserLoginDTO> CrearUsuarioContraseña(UserLoginDTO crearUsuario,int idSucursal);
         Task <string> Autenticacion(UserLoginDTO user);
         Task<UserLoginDTO> GetUsuarioSeguridad(UserLoginDTO seguridad);
    }
}