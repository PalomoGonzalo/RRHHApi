using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using RRHH.Core.DTOS;
using RRHH.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RRHH.Infraestructura.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private readonly IConfiguration _configuration;

        public UsuarioRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<UsuarioDTO>> ObtenerUsuarios()
        {
            using IDbConnection db = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            string sql = @"SELECT * FROM Usuario WHERE Activo=1";

            IEnumerable<UsuarioDTO> usuarios = await db.QueryAsync<UsuarioDTO>(sql).ConfigureAwait(false);

            return usuarios;
        }


        public async Task<UsuarioDTO> ObtenerUsuarioPorId(int id)
        {
            using IDbConnection db = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string query = $"SELECT * FROM Usuario WHERE IdUsuario=@id and Activo=1";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("id", id,DbType.Int64);

            var user= await db.QueryFirstOrDefaultAsync<UsuarioDTO>(query, dp);

            return user;

        }

        public String GenerarToken(UserLoginDTO seguridad)
        {
            JwtSecurityTokenHandler tokenhandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("usuario", seguridad.IdUsuario.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"])), SecurityAlgorithms.HmacSha256)
            };

            SecurityToken token = tokenhandler.CreateToken(tokenDescriptor);
            String encodeJwt = tokenhandler.WriteToken(token);

            return encodeJwt;
        }


    }
}
