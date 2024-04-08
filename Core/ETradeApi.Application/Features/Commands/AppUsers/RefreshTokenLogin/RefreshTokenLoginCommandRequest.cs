using MediatR;

namespace ETradeApi.Application.Features.Commands.AppUsers.RefreshTokenLogin
{
	public class RefreshTokenLoginCommandRequest : IRequest<RefreshTokenLoginCommandResponse>
	{
        public string RefreshToken { get; set; }
    }
}
