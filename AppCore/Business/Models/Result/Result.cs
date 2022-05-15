using AppCore.Business.Models.Result.Bases;

namespace AppCore.Business.Models.Result
{
    public class Result
    {
        public bool IsSuccessful { get; } // readonly : sadece constructor üzerinden set edilebilir
        public string Message { get; set; }

        public Result(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }

    public class Result<TResultType> : Result, IResultData<TResultType>
    {
        public TResultType Data { get; }

        public Result(bool isSuccessful, string message, TResultType data) : base(isSuccessful, message)
        {
            Data = data;
        }
    }
}