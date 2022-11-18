namespace RRHH.Core.Interfaces
{
    public interface IContraseñaRepositorio
    {
         string Hash(string password);
         bool CheckHash(string hash, string password);
    }
}