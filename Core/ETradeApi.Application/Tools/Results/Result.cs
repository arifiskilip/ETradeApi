namespace ETradeApi.Application.Tools.Results;
public class Result : IResult
{
	public bool Success { get; }

	public string Message { get; }


	public Result(bool succes, string message) : this(succes)
	{
		Message = message;
	}

	public Result(bool success)
	{
		Success = success;
	}
}
