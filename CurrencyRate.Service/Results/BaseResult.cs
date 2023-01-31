namespace CurrencyRate.Service.Results
{
    public class BaseResult
    {
        public BaseResult()
        {
        }

        public BaseResult(Error error)
        {
            AddError(error);
        }

        public IList<Error> Errors { get; } = new List<Error>();

        public bool IsSuccess { get; private set; } = true;

        public void AddError(Error error)
        {
            IsSuccess = false;
            Errors.Add(error);
        }
    }

    public class Error
    {
        public Error(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; }

        public string Message { get; }
    }
}
