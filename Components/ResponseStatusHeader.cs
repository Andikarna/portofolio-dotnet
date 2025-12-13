using Microsoft.AspNetCore.Mvc;

namespace PTP.Components
{
    public class ResponseStatusHeader
    {
        public IActionResult BuildResponse(object result)
        {
            var statusProp = result.GetType().GetProperty("Status");
            var dataProp = result.GetType().GetProperty("Data");

            var statusValue = statusProp?.GetValue(result)?.ToString() ?? "500";
            var dataValue = dataProp?.GetValue(result);

            return statusValue switch
            {
                "200" => new OkObjectResult(result),

                "204" => new NoContentResult(),

                "400" => new BadRequestObjectResult(result),

                "401" => new UnauthorizedObjectResult(result),

                "404" => new NotFoundObjectResult(result),

                "500" => new ObjectResult(result)
                {
                    StatusCode = 500
                },

                _ => new ObjectResult(result)
                {
                    StatusCode = 500
                }
            };
        }
    }
}
