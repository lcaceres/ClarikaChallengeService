using System;

namespace ClarikaChallengeService.Infraestructure.Exceptions
{
    [Serializable]
    public class ApplicationArgumentException : ArgumentException
    {
        public ApplicationArgumentException(string paramName, string message) : base(message, paramName)
        {
        }

        public ApplicationArgumentException(string message) : base(message)
        {
        }
    }
}
