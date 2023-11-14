namespace ClarikaChallengeService.WebApi.DTOs
{
    public class ErrorResponse
    {
        public string ExceptionMessage { get; set; }
        public string Message { get; set; }
        public Type ExceptionType { get; set; }
        public ErrorResponse()
        {

        }
        public ErrorResponse(string message)
        {
            Message = message;
            ExceptionMessage = message;
        }
    }
}
