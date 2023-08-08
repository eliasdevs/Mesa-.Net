using Fluxor;
using Refit;

namespace Mesa.Juegos.State.Shared
{
    public abstract class EffectBase<T> : Effect<T>
    {
        public abstract Task ExecuteAsync(T action, IDispatcher dispatcher);


        public override async Task HandleAsync(T action, IDispatcher dispatcher)
        {
            try
            {
                await ExecuteAsync(action, dispatcher);
            }
            catch (ApiException ex)
            {
                var errorContent = await ex.GetContentAsAsync<ApiExceptionResult>();

                if (ex.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    dispatcher.Dispatch(new SetBackendError("403", null, "Forbbiden", "No tienes permiso a realizar la accion especificada", null, null));
                    await OnException(ex, dispatcher);
                    return;
                }

                if ((ex.StatusCode == System.Net.HttpStatusCode.BadRequest || ex.StatusCode == System.Net.HttpStatusCode.NotFound) && errorContent != null)
                {
                    dispatcher.Dispatch(new SetBackendError(ex.StatusCode.ToString(), errorContent.ErrorTypeCode, errorContent.ErrorTypeName, errorContent.Message, errorContent.RelatedObject, errorContent.RelatedField));
                    await OnException(ex, dispatcher);
                    return;
                }

                dispatcher.Dispatch(new SetBackendError(ex.StatusCode.ToString(), null, null, ex.Content ?? "", "", ""));
                await OnException(ex, dispatcher);
            }
        }

        /// <summary>
        /// Permite al a los efecs especificar que accion de state llamar cuando ocurre un error, por ejemplo esconder un loader que quedo activo.
        /// </summary>
        /// <param name="ex"></param>
        public abstract Task OnException(ApiException ex, IDispatcher dispatcher);

        /// <summary>
        /// Objeto para brindar m'as detalles del error en la llamada REST
        /// </summary>
        private class ApiExceptionResult
        {

            public ApiExceptionResult()
            {
                Message = "";
                RelatedField = "";
                RelatedObject = "";
            }

            /// <summary>
            /// Codigo del tipo de error
            /// </summary>
            public int? ErrorTypeCode { get; set; }

            /// <summary>
            /// Nombre del tipo de error
            /// </summary>
            public string? ErrorTypeName { get; set; }

            /// <summary>
            /// El mensaje d eerror
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// La claser en la que se detecto la excepcion
            /// </summary>
            public string RelatedObject { get; set; }
            /// <summary>
            /// El campo relacionado al error
            /// </summary>
            public string RelatedField { get; set; }
        }
    }
}