using Dapper;
using Microsoft.Data.Sqlite;
using TorneoGalactico.Api.Data.Conection;
using TorneoGalactico.Api.DTOs;
using TorneoGalactico.Api.Models;
using static TorneoGalactico.Api.DTOs.Dtos;
using static TorneoGalactico.Api.Exceptions.AppExceptions;

namespace TorneoGalactico.Api.Services
{
    public class SpecieService(ISqliteConnection conn) : ISpecieService
    {
        public async Task<Dtos.SpeciesResponse> CreateAsync(Dtos.CreateSpeciesRequest request)
        {
            var name = request.Name?.Trim();
            var specialAbility = request.SpecialAbility?.Trim();

            if (string.IsNullOrWhiteSpace(name))
                throw new BadRequestException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(specialAbility))
                throw new BadRequestException("La habilidad especial es obligatoria.");
            if (request.PowerLevel < 0)
                throw new BadRequestException("El nivel de poder no puede ser negativo.");

            using var db = conn.onCreate();

            try
            {
                var id = await db.ExecuteScalarAsync<long>(
                    """
                    INSERT INTO Species (Name, PowerLevel, SpecialAbility)
                    VALUES (@Name, @PowerLevel, @SpecialAbility);
                    SELECT last_insert_rowid();
                    """,
                    new { Name = name, request.PowerLevel, SpecialAbility = specialAbility }
                );

                return new SpeciesResponse((int)id, name, request.PowerLevel, specialAbility);
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 19)
            {
                throw new ConflictException($"Ya existe una especie llamada '{name}'.");
            }
        }

        public async Task<List<SpeciesResponse>> GetAllAsync()
        {
            using var db = conn.onCreate();
            var species = await db.QueryAsync<Species>("SELECT * FROM Species ORDER BY Name COLLATE NOCASE");
            return species.Select(s => new SpeciesResponse(s.Id, s.Name, s.PowerLevel, s.SpecialAbility)).ToList();
        }
    }
}
