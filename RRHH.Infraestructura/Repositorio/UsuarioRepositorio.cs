using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RRHH.Core.DTOS;
using RRHH.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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


    }
}
