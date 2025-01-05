using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WildLeague.API.DTOs;
using WildLeague.Application.Interfaces.Services;
using WildLeague.Domain.ValueObjects;

namespace WildLeague.API.Controllers
{
	public class LeagueController : BaseController<LeagueController>
	{
		private readonly ILeagueService _leagueService;
		private readonly IMapper _mapper;

		public LeagueController(
			IMediator mediator, 
			IMapper mapper,
			ILeagueService leagueService) : base(mediator)
		{
			_leagueService = leagueService;
			_mapper = mapper;
		}

		[HttpPost]
		[Route("createNewLeague")]
		public async Task<ActionResult<CreateNewLeagueResponseDto>> CreateNewLeague([FromBody]CreateNewLeagueRequestDto dto)
		{
			var leagueName = new LeagueName(dto.Name.Value);
			var league = _leagueService.Create(leagueName, dto.NumberOfTeams);

			var response = new CreateNewLeagueResponseDto()
			{
				LeagueAsString = league.ToString()
			};

			return Ok(response);
		}
	}
}
