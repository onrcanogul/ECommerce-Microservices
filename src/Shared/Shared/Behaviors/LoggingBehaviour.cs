using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Behaviors
{
    public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest,TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull , IRequest<TResponse>
        where TResponse : notnull
    {

        

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation($"[START] Handle Request = {typeof(TRequest).Name} Response = {typeof(TResponse).Name} - RequestData = {request}");

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;
            if(timeTaken.Seconds > 3)
                logger.LogWarning($"[PERFORMANCE] Handle Request = {typeof(TRequest).Name} took {timeTaken.Seconds}");

            logger.LogInformation($"[END] Handled {typeof(TRequest).Name} with {typeof(TResponse).Name}");
            return response;
        }
    }
}
