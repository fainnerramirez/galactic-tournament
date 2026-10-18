namespace TorneoGalactico.Api.Exceptions
{
    public class AppExceptions
    {
        public class NotFoundException(string message) : Exception(message);
        public class BadRequestException(string message) : Exception(message);
        public class ConflictException(string message) : Exception(message);
    }
}
