using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace invoice_manager.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            if (context.Exception is not DbUpdateException) return;
            if (context.Exception.InnerException is MySqlException {ErrorCode: MySqlErrorCode.DuplicateKeyEntry})
            {
                context.Result = new ConflictResult();
            }
        }
    }
}