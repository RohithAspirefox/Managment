namespace Management.Common.Models.ApiResponse
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string? Error { get; set; }

        public string? Message{get; set;}
    }
}