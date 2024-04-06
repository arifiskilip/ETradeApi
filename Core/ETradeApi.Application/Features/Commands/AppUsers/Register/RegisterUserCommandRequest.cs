using MediatR;

namespace ETradeApi.Application.Features.Commands.AppUsers.Register
{
	public class RegisterUserCommandRequest:IRequest<RegisterUserCommandResponse>
	{
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
