﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Shared.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            //log
            logger.LogError("Error Message : {exceptionMeesage}, Time of occurence {time}",exception.Message, DateTime.UtcNow);


            //anonymous object
            (string Detail, string Title, int StatusCode) details = exception switch
            {
                InternalServerException => (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
                ),
                ValidationException => (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                BadRequestException => (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                NotFoundException => (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status404NotFound
                ),
                _ => (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };


            ProblemDetails problemDetails = new()
            {
                Title = details.Title,
                Status = details.StatusCode,
                Detail = details.Detail,
                Instance = context.Request.Path
            };

            //add custom problem details
            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

            if(exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors",  validationException.Message);
            }

            await context.Response.WriteAsJsonAsync(problemDetails,cancellationToken);
            return true;


        }
    }
}
