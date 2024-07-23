using E_Commerce.APIs.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APIs.Controllers
{
	[Route("errors/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi =true)]
	public class ErrorsController : ControllerBase
	{
		[HttpGet]
		public ActionResult Error(int code)
		{
			switch (code)
			{
				case 404:
					return NotFound(new ApiResponse(404));
				case 401:
					return Unauthorized(new ApiResponse(401));
					default:
					return StatusCode(code);
						
			}
		}
	}
}
