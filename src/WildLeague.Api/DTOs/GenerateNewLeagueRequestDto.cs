namespace WildLeague.API.DTOs
{
	public class GenerateNewLeagueRequestDto
	{
		public required string LeagueName { get;set; }
		public required int NumberOfTeams { get;set; }
	}
}
