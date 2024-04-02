namespace ETradeApi.Infrastructure.Results
{
	public interface IDataResult<T> : IResult
	{
		T Data { get; }
	}
}
