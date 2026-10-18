using Microsoft.AspNetCore.Mvc;
using static TorneoGalactico.Api.Exceptions.AppExceptions;

namespace TorneoGalactico.Api.Middleware
{
    public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var (status, title) = ex switch
                {
                    NotFoundException => (StatusCodes.Status404NotFound, "No encontrado"),
                    BadRequestException => (StatusCodes.Status400BadRequest, "Solicitud inválida"),
                    ConflictException => (StatusCodes.Status409Conflict, "Conflicto"),
                    _ => (StatusCodes.Status500InternalServerError, "Error interno")
                };

                if (status == StatusCodes.Status500InternalServerError)
                    logger.LogError(ex, "Error no controlado");

                context.Response.StatusCode = status;
                await context.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = status,
                    Title = title,
                    Detail = status == 500 ? "Ocurrió un error inesperado." : ex.Message
                });
            }
        }
    }
}