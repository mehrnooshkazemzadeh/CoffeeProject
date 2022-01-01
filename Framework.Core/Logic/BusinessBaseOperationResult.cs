using System.Collections.Generic;
using System.Linq;

namespace Framework.Core.Logic
{
    public class BusinessBaseOperationResult
    {
        public long? Count { get; set; }
        public List<string> Messages { get; }
        public BusinessBaseOperationResult()
        {
            Messages = new List<string>();

        }
        public string AllMessages => Messages == null || !Messages.Any() ? "" : Messages.Aggregate((x, current) => x + "\n" + current);
        public OperationResultStatus ResultStatus { get; set; }
        public void SetErrorMessage(string message)
        {
            Messages.Clear();
            Messages.Add(message);
            ResultStatus = OperationResultStatus.Error;
        }

    }
}
