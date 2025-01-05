using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WildLeague.API.Controllers
{
	[Route("api/v{version:apiVersion}/[controller]")]
	public abstract class BaseController<TController> : Controller
	{
		protected IMediator Mediator { get; }
		//protected JsonSerializerOptions SerializingOptions => Serializing.Options;

		public BaseController(IMediator mediator)
		{
			Mediator = mediator;
		}
		protected async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
		{
			return await SendAsync(request, default);

		}
		protected async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
		{
			return await Mediator.Send(request, cancellationToken);
		}

		protected void LogInformation(string message)
		{
			//LogHelper.GetContextLogger(Request).Information(message);
		}

		protected void LogWarning(string message)
		{
			//LogHelper.GetContextLogger(Request).Warning(message);
		}

		protected void LogError(string message)
		{
			//LogHelper.GetContextLogger(Request).Error(message);
		}

		protected void LogDebug(string message)
		{
			//LogHelper.GetContextLogger(Request).Debug(message);
		}
	}
}
