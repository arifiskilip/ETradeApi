using ETradeApi.Application.Dtos;
using ETradeApi.Application.Tools.Results;

namespace ETradeApi.Application.Features.Commands.AppUsers.Login
{
	public class LoginUserCommandResponse
	{
        public IDataResult<Token> Data { get; set; }
    }
}
