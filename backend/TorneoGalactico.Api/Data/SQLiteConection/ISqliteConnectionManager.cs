using System.Data;
namespace TorneoGalactico.Api.Data.Conection
{
    public interface ISqliteConnection
    {
        public IDbConnection onCreate();
    }
}