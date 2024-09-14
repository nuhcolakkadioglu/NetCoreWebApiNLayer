using App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Services.Filters
{
    public class NotFoundFilter<T, TId>(IGenericRepository<T, TId> _genericRepository) : Attribute, IAsyncActionFilter where T : class where TId : struct
    {


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next();
                return;
            }

            if (!int.TryParse(idValue.ToString(), out int id))
            {
                await next();
                return;

            }

            var anyEntity = await _genericRepository.GetByIdAsync(id);

            if (anyEntity is null)
            {
                var entityName = typeof(T).Name;
                var actionName = context.ActionDescriptor.RouteValues["action"];

                var result = ServiceResult.Fail($"kayıt bulunamadı. {entityName} (${actionName})");

                context.Result = new NotFoundObjectResult(result);
                return;
            }


            await next();
        }
    }
}
