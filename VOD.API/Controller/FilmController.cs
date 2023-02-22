
namespace VOD.API.Controller;
[Route("api/[controller]")]
[ApiController]

public class FilmController : ControllerBase
{
	private readonly IDbService _db;
	public FilmController(IDbService db)
	{
		_db = db;
	}
	// GET: GET ALL
	[HttpGet]
	public async Task<IResult> Get(bool freeOnly)
	{
		try
		{
			_db.IncludeRef<FilmGenre>();
			_db.IncludeRef<SimilarFilm>();
			_db.Include<Film>();
			List<FilmDTO>? films = freeOnly ?
				await _db.GetAsync<Film, FilmDTO>(c => c.Free.Equals(freeOnly)) :
				await _db.GetAsync<Film, FilmDTO>();

			return Results.Ok(films);
		}
		catch
		{
		}

		return Results.NotFound();

	}
	//// GET: BY ID
	[HttpGet("{id}")]
	public async Task<IResult> Get(int id)
	{
		try
		{
			_db.IncludeRef<FilmGenre>();
			_db.IncludeRef<SimilarFilm>();
			_db.Include<Film>();

			var exists = await _db.AnyAsync<Film>(d => d.Id.Equals(id));
			if (!exists) return Results.NotFound("Filmen hittades inte.");

			var film = await _db.SingleAsync<Film, FilmDTO>(c => c.Id.Equals(id));

			return Results.Ok(film);
		}
		catch
		{
		}
		return Results.NotFound();
	}

		//// POST: CREATE FILM
		[HttpPost]
		public async Task<IResult> Post([FromBody] CreateFilmDTO dto)
		{
			try
			{

			if (dto == null) return Results.BadRequest();

				var film = await _db.AddAsync<Film, CreateFilmDTO>(dto);

				var success = await _db.SaveChangesAsync();

				if (!success) return Results.BadRequest();

				return Results.Created(_db.GetURI<Film>(film), film);
			}
			catch
			{
			}

			return Results.BadRequest();
		}

	//// PUT: EDIT FILM
	[HttpPut("{id}")]
	public async Task<IResult> Put(int id, [FromBody] FilmEditDTO dto)
	{
		try
		{
            if (dto == null) return Results.BadRequest("Ingen film hittades");
			if (!id.Equals(dto.Id)) return Results.BadRequest("Ingen film med detta ID hittades");

			var exists = await _db.AnyAsync<Director>(i => i.Id.Equals(dto.DirectorId));
			if (!exists) return Results.NotFound("Kunde ej hitta director");

			exists = await _db.AnyAsync<Film>(c => c.Id.Equals(id));
			if (!exists) return Results.NotFound("Kunde ej hitta entitet");

			_db.Update<Film, FilmEditDTO>(dto.Id, dto);

			var success = await _db.SaveChangesAsync();

			if (!success) return Results.BadRequest();

			return Results.NoContent();
		}
		catch
		{
		}

		return Results.BadRequest("Kunde ej uppdatera");

	}
	// // DELETE 

	[HttpDelete("{id}")]
	public async Task<IResult> Delete(int id)
	{
		try
		{
            await _db.DeleteSimilarFilmsAsync(id);
			await _db.DeleteFilmGenreAsync(id);
            var success = await _db.DeleteAsync<Film>(id);
			await _db.DeleteFilmGenreAsync(id);
			
			if (!success) return Results.NotFound();

			success = await _db.SaveChangesAsync();

			if (!success) return Results.BadRequest();

			return Results.NoContent();
		}
		catch
		{
		}

		return Results.BadRequest();
	}
}


