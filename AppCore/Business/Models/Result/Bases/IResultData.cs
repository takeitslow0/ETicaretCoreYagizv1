namespace AppCore.Business.Models.Result.Bases
{
    public interface IResultData<out TResultType>
    {
        TResultType Data { get; }
    }
}
