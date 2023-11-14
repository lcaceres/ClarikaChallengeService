namespace ClarikaChallengeService.Infraestructure.Exceptions
{
    [Serializable]
    public class InvalidApplicationOperationException : InvalidOperationException
    {
        public InvalidApplicationOperationException(string message)
            : base(message)
        {
        }
    }
}
