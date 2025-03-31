﻿using Ecom.Api.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("error/{statuscode}")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int statuscode)
        {
            return new ObjectResult(new ResponseApi(statuscode));
        }
    }
}
