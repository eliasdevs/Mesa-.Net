using Mesa_SV.Exceptions;
using Mesa_SV.Exceptions.MesaBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV.Filter
{
    public  class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is MesaForbiddenException)
            {
                context.Result = new ForbidResult();
            }

            if (context.Exception is MesaBaseException ex)
            {

                ApiExceptionResult result = new ApiExceptionResult();
                result.ErrorTypeCode = ex.GetExceptionTypeCode();
                result.ErrorTypeName = ex.GetExceptionTypeDescription();
                result.RelatedObject = ex.RelatedObject.FullName;
                result.Message = ex.Message;
                result.RelatedField = ex.RelatedField;


                _logger.LogError("Tipo de error: {ErrorType},  ErrorTypeCode: {ErrorTypeCode}, ErrorTypeName: {ErrorTypeName}, RelatedField: {RelatedField}, RelatedObject: {RelatedObject}, Message: {Message}",
                    ex.GetErrorType().ToString(), result.ErrorTypeCode, result.ErrorTypeName, result.RelatedField, result.RelatedObject, result.Message);

                if (ex.GetErrorType() == HttpErrorType.Client)
                    context.Result = new BadRequestObjectResult(result);

                if (ex.GetErrorType() == HttpErrorType.NotFound)
                    context.Result = new NotFoundObjectResult(result);
            }

           

        }
    }
}
