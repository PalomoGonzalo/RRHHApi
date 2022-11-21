namespace RRHH.Core.Interfaces
{
    public interface IContraseñaHassRepositorio
    {
         string Hash(string password);
         bool CheckHash(string hash, string password);
    }
}