using System.Net;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using RRHH.Core.DTOS;
using RRHH.Core.Interfaces;
using System.Data;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RRHH.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _user;
        private readonly IUsuarioContraseñaRepositorio _contraseña;

        public UsuarioController(IUsuarioRepositorio user, IUsuarioContraseñaRepositorio contraseña)
        {
            _user = user;
            _contraseña = contraseña;
        }


        [Authorize]
        [HttpGet("ObtenerUsuarios")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios =await _user.ObtenerUsuarios();
            return Ok(usuarios);    
            
        }


        [HttpPost("CrearContraseñaUsuario")]
        public async Task<IActionResult> CrearContraseñaUsuario([FromBody]UserLoginDTO user)
        {
            if(user==null)
            {
                return BadRequest("Erroe en el payload");
            }

            var usuario = await _user.ObtenerUsuarioPorId(user.IdUsuario);
            if(usuario == null)
            {
                return BadRequest("No existe el usuario en el sistema");
            }

            var contraseñaUsuarioCreado = await _contraseña.CrearUsuarioContraseña(user,usuario.IdSucursal);

            return Ok($"Se creo correctamente el usuario {contraseñaUsuarioCreado.IdUsuario}");

        }

        [HttpPost("Login")]
        public  async Task <IActionResult> Login([FromBody] UserLoginDTO user)
        {
            if (user == null)
                return BadRequest("Error datos no validos");
            var token = await _contraseña.Autenticacion(user);
            if (token == null)
            {
                return BadRequest("error en el usuario o contrase�a ");
            }
            
            return Ok(token);
        }




    }
}
