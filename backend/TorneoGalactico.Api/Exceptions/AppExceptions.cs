namespace TorneoGalactico.Api.Exceptions
{
    public class AppExceptions
    {
        public class NotFoundCombatException(string message) : Exception(message);
        public class BadRequestCombatException(string message) : Exception(message);
        public class ConflictCombatException(string message) : Exception(message);
    }
}
