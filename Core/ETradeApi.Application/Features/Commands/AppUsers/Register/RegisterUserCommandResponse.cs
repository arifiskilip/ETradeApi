using ETradeApi.Application.Dtos;
using ETradeApi.Application.Tools.Results;

namespace ETradeApi.Application.Features.Commands.AppUsers.Register
{
	public class RegisterUserCommandResponse
	{
		public IDataResult<Token> Data { get; set; }
	}
}
