namespace E_Commerce.APIs.Errors
{
	public class ApiResponseServerError:ApiResponse
	{
		public string? Details { get; set; }

		public ApiResponseServerError(int statusCode, string? ErrorMessage = null,string? details=null)
			:base(statusCode, ErrorMessage)
        {
			Details = details;
        }
    }
}
