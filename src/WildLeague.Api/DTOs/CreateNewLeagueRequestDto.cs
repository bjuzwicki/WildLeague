namespace WildLeague.API.DTOs
{
	public class CreateNewLeagueRequestDto
	{
		public required LeagueNameDto Name { get;set; }
		public required int NumberOfTeams { get;set; }
	}
}
