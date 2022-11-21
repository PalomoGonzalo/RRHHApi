using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using RRHH.Core.Interfaces;
using RRHH.Infraestructura.Options;

namespace RRHH.Infraestructura.Repositorio
{
    public class ContraseñaHassRepositorio : IContraseñaHassRepositorio
    {
        
        private readonly ContraseñaOpciones _config;
        
        public ContraseñaHassRepositorio(IOptions<ContraseñaOpciones> config)
        {
            _config = config.Value;
        }


        public bool CheckHash(string hash, string password)
        {

            if (String.IsNullOrEmpty(password))
                throw new ArgumentException(nameof(password));

            if (String.IsNullOrEmpty(hash))
                throw new ArgumentException(nameof(hash));

            var parts = hash.Split('.', 3);
            if (parts.Length != 3)
            {
                throw new FormatException("Inesperado formato de hash");
            }

            var iteraciones = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);


            using (var algorithm = new Rfc2898DeriveBytes(
             password,
             salt,
             iteraciones
            ))
            {
                var keyToCheck = algorithm.GetBytes(_config.KeySize);
                return keyToCheck.SequenceEqual(key);
            }


        }

        public string Hash(string password)
        {
            if (String.IsNullOrEmpty(password))
                throw new ArgumentException(nameof(password));

            //PBKDF2 IMPLEMENTACION
            using (var algorithm = new Rfc2898DeriveBytes(
             password,
             _config.SaltSize,
             _config.Iteraciones
            ))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_config.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);
                return $"{_config.Iteraciones}.{salt}.{key}";
            }
        }

    }
}