using Dapper;
using TorneoGalactico.Api.Data.Conection;

namespace TorneoGalactico.Api.Data.Database
{
    public class DatabaseInitializer(ISqliteConnection connection)
    {
        public void Initialize()
        {
            using var db = connection.onCreate();

            db.Execute("PRAGMA foreign_keys = ON;");

            db.Execute("""
                    CREATE TABLE IF NOT EXISTS Species (
                        Id             INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name           TEXT    NOT NULL COLLATE NOCASE UNIQUE,
                        PowerLevel     INTEGER NOT NULL,
                        SpecialAbility TEXT    NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Battles (
                        Id         INTEGER PRIMARY KEY AUTOINCREMENT,
                        SpeciesAId INTEGER NOT NULL REFERENCES Species(Id),
                        SpeciesBId INTEGER NOT NULL REFERENCES Species(Id),
                        WinnerId   INTEGER NOT NULL REFERENCES Species(Id),
                        FoughtAt   TEXT    NOT NULL
                    );
             """);
        }
    }
}