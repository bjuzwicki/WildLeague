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
		//private readonly IMapper _mapper;

		public LeagueController(
			IMediator mediator, 
			//IMapper mapper,
			ILeagueService leagueService) : base(mediator)
		{
			_leagueService = leagueService;
		//	_mapper = mapper;
		}

		[HttpPost]
		[Route("generateNewLeague")]
		public async Task<ActionResult<GenerateNewLeagueResponseDto>> GenerateNewLeague([FromBody]GenerateNewLeagueRequestDto dto)
		{
			var leagueName = new LeagueName(dto.LeagueName);
			var league = _leagueService.Generate(leagueName, dto.NumberOfTeams);

			var response = new GenerateNewLeagueResponseDto()
			{
				LeagueAsString = league.ToString()
			};

			return Ok(response);
		}
	}
}
