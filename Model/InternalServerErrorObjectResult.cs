using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Model
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object eror)
            : base(eror) 
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
