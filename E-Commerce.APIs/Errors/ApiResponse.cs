
namespace E_Commerce.APIs.Errors
{
	public class ApiResponse
	{
        public int StatusCode { get; set; }
		public string ErrorMessage { get; set; }
        public ApiResponse(int StatusCode, string? ErrorMessage=null)
        {
            this.StatusCode = StatusCode;
            if (ErrorMessage == null)
                ErrorMessage= GetDefaultMessageForStatusCode(StatusCode);
            this.ErrorMessage = ErrorMessage;

        }

		private string GetDefaultMessageForStatusCode(int statusCode)
		{
			return statusCode switch
			{
				400 => "A bad request , you made",
				401 => "Authorized , you are not ",
				404 => "Resources not found",
				500 => "Errors Are the path in the dark side",
				_ => null
			};

		}
	}
}
