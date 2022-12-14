using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RRHH.Core.DTOS;

namespace RRHH.Core.Interfaces
{
    public interface IUsuarioContrase├▒aRepositorio
    {
         Task<UserLoginDTO> ObtenerUsuarioContrase├▒a (UserLoginDTO user);
         Task<UserLoginDTO> CrearUsuarioContrase├▒a(UserLoginDTO crearUsuario,int idSucursal);
         Task <string> Autenticacion(UserLoginDTO user);
         Task<UserLoginDTO> GetUsuarioSeguridad(UserLoginDTO seguridad);
    }
}