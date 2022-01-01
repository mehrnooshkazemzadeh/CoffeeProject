using System;

namespace Framework.Core.Tools
{
    public static class ExceptionParser
    {
        public static string Parse(Exception ex)
        {
            var innerException = ex;
            var message = "";
            while (innerException != null)
            {
                message += innerException.Message + Environment.NewLine;
                innerException = innerException.InnerException;
            }
            return message;
        }
    }
}
