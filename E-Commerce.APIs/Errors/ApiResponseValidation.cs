namespace E_Commerce.APIs.Errors
{
	public class ApiResponseValidation:ApiResponse
	{
        public IEnumerable<string> Errors { get; set; }
        public ApiResponseValidation():base(400)
        {
            Errors = new List<string>();    
        }
    }
}
