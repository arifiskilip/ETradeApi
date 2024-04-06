namespace ETradeApi.Application.Tools.Results;

public interface IResult
{
	bool Success { get; }
	string Message { get; }
}
