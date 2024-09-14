using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using App.Domain.Exceptions;

namespace App.API.ExceptionHandler
{
    public class CriticalExceptionHandler() : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is CriticalException)
            {
                Console.WriteLine("SMS GÖNDERİLDİ CriticalException İÇİN");
            }

            return ValueTask.FromResult(false);
        }
    }
}
