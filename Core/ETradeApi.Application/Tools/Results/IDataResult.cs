namespace ETradeApi.Application.Tools.Results;
public interface IDataResult<T> : IResult
{
	T Data { get; }
}
