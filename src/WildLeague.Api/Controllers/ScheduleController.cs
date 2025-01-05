using MediatR;
using WildLeague.API.Controllers;

namespace WildLeague.Api.Controllers
{
	public class ScheduleController : BaseController<ScheduleController>
	{
		public ScheduleController(IMediator mediator) 
			: base(mediator)
		{
		}
	}
}
