using ECommerceG03.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ECommerceG03.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInSeconds;

        public RedisCacheAttribute(int durationInSeconds = 60)
        {
            _durationInSeconds = durationInSeconds;
        }
        // Work before Endpoint and After Endpoint Run
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get CacheService
            var cacheService = context.HttpContext.RequestServices.GetRequiredService< ICacheService>();

            // URL : http://localhost:3000/api/Products
            // URL : http://localhost:3000/api/Products/ProductType=2
            // URL : http://localhost:3000/api/Products/ProductType=2&ProductBrand=3
            // URL : http://localhost:3000/api/Products/2

            // cacheKey = http://localhost:3000/api/Products

            var cacheKey = CreateCacheKey(context.HttpContext.Request);
            var data = await cacheService.GetDataAsync(cacheKey);

            if (!string.IsNullOrEmpty(data))
            {
                context.Result = new ContentResult()
                {
                    Content = data,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };

                return;
            }
            var excutedContext = await next.Invoke();
            if(excutedContext.Result is OkObjectResult {Value: not null } ok)
            {
                // TODO Reminder
                await cacheService.SetDataAsync(cacheKey, ok.Value ,TimeSpan.FromSeconds(_durationInSeconds));
            }

            
        }

        private static string CreateCacheKey(HttpRequest request) 
        {
            var key = new StringBuilder();
            key.Append(request.Path);

            if (request.Query.Any())
            {
                // Key = BaseUrl/api/Product
                key.Append('?');
                foreach (var (k , v) in request.Query.OrderBy(x => x.Key))
                {
                    key.Append(k).Append("=").Append(v).Append('&');
                }
            }

            return key.ToString();
        }
    }
}
