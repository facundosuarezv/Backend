using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ErrorController : ControllerBase
{
    [HttpGet] 
    [Route("/error")]
    public IActionResult HandleError() => Problem();
}

