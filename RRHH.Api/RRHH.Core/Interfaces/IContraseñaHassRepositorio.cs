namespace RRHH.Core.Interfaces
{
    public interface IContrase├▒aHassRepositorio
    {
         string Hash(string password);
         bool CheckHash(string hash, string password);
    }
}