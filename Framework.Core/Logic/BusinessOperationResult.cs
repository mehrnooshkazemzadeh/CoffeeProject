using System.Collections.Generic;

namespace Framework.Core.Logic
{
    public class BusinessOperationResult<T> : BusinessBaseOperationResult
    {

        public BusinessOperationResult()
        {
        }

        public T ResultEntity { get; set; }

        public void SetMessage(string message, T resultEntity, OperationResultStatus resultStatus)
        {
            Messages.Clear();
            Messages.Add(message);
            ResultEntity = resultEntity;
            ResultStatus = resultStatus;
        }
        public BusinessOperationResult<T> SetSingleMessage(string message, OperationResultStatus status)
        {
            Messages.Add(message);
            ResultStatus = status;
            return this;
        }
        public BusinessOperationResult<T> SetSingleMessage(string message, OperationResultStatus status, T result)
        {
            Messages.Add(message);
            ResultEntity = result;
            ResultStatus = status;
            return this;
        }
        public void SetErrorMessage(string message, T resultEntity)
        {
            Messages.Clear();
            Messages.Add(message);
            ResultEntity = resultEntity;
            ResultStatus = OperationResultStatus.Error;
        }


        public void SetSuccessResult(T resultEntity)
        {
            Messages.Clear();
            ResultEntity = resultEntity;
            ResultStatus = OperationResultStatus.Successful;
        }

        public void SetResult(string message, OperationResultStatus resultStatus, T resultEntity)
        {
            Messages.Clear();
            Messages.Add(message);
            ResultEntity = resultEntity;
            ResultStatus = resultStatus;
        }

        public void SetResult(IEnumerable<string> messages, OperationResultStatus resultStatus, T resultEntity)
        {
            Messages.Clear();
            if (messages != null)
            {
                foreach (var message in messages)
                {
                    Messages.Add(message);
                }
            }
            ResultEntity = resultEntity;
            ResultStatus = resultStatus;
        }
    }
}
