using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RRHH.Core.DTOS;
using RRHH.Core.Interfaces;

namespace RRHH.Infraestructura.Repositorio
{
    public class UsuarioContraseñaRepositorio : IUsuarioContraseñaRepositorio
    {

        private readonly IConfiguration _config;
        private readonly IUsuarioRepositorio _user;
        private readonly IContraseñaHassRepositorio _passwordHash;




        public UsuarioContraseñaRepositorio(IConfiguration config, IUsuarioRepositorio user, IContraseñaHassRepositorio passwordHash)
        {
            _config = config;
            _user = user;
            _passwordHash = passwordHash;
        }

        public async Task<string> Autenticacion(UserLoginDTO user)
        {
            UserLoginDTO seguridadUser = await ObtenerUsuarioContraseña(user);

            if (user == null)
            {
                throw new Exception ("Error en el payload");
            }
            if(seguridadUser == null)
            {
                return null;
            }
            
            if(!(_passwordHash.CheckHash(seguridadUser.Contraseña, user.Contraseña)))
            {
                return null;
            }

            string token= _user.GenerarToken(seguridadUser);
            return token;
        }

        public async Task<UserLoginDTO> CrearUsuarioContraseña(UserLoginDTO crearUsuario,int idSucursal)
        {
            if (crearUsuario == null)
                throw new ArgumentNullException();
            
            var passHash=_passwordHash.Hash(crearUsuario.Contraseña);

            using IDbConnection db = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));

            string sql = @"INSERT INTO Usuario_Password (IdUsuario,IdSucursal,Contraseña)
                            VALUES(@usuario,@IdSucursal,@contraseña)";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("usuario", crearUsuario.IdUsuario, DbType.Int32);
            dp.Add("IdSucursal", idSucursal, DbType.String);
            dp.Add("contraseña", passHash, DbType.String);
           



            int row = await db.ExecuteAsync(sql, dp);

            if (row ==0)
            {
                 throw new Exception("No se logro crear correctamente el usuario");
            }
            return crearUsuario;
        }

        public Task<UserLoginDTO> GetUsuarioSeguridad(UserLoginDTO seguridad)
        {
            throw new NotImplementedException();
        }

        public async Task<UserLoginDTO> ObtenerUsuarioContraseña(UserLoginDTO user)
        {
           using IDbConnection db = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));

            string query = $@"SELECT * FROM Usuario_Password WHERE IdUsuario=@user";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("user", user.IdUsuario, DbType.Int32);

            return await db.QueryFirstOrDefaultAsync<UserLoginDTO>(query, dp);
        }
    }
}