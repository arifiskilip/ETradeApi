using ETradeApi.Application.Dtos;
using ETradeApi.Application.Tools.Results;

namespace ETradeApi.Application.Features.Commands.AppUsers.RefreshTokenLogin
{
	public class RefreshTokenLoginCommandResponse
	{
        public IDataResult<Token> Data { get; set; }
    }
}
