using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RRHH.Core.DTOS
{
    public class UserLoginDTO
    {
        public int IdUsuario { get; set; }
        public string Contraseña { get; set; }
    }
}