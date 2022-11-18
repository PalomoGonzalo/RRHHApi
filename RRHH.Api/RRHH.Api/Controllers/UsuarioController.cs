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

        public UsuarioController(IUsuarioRepositorio user)
        {
            _user = user;
        }
        [Authorize]

        [HttpGet("ObtenerUsuarios")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios =await _user.ObtenerUsuarios();
            return Ok(usuarios);    
            
        }


    }
}
