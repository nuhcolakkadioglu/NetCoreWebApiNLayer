﻿using App.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

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
