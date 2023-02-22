using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VOD.API.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class GenreController : ControllerBase
	{
		private readonly IDbService _db;
		public GenreController(IDbService db)
		{
			_db = db;
		}
		// GET: GET ALL
		[HttpGet]
		public async Task<IResult> Get()
		{
			try
			{
				_db.IncludeRef<FilmGenre>();
				_db.Include<Genre>();
				_db.IncludeRef<SimilarFilm>();
				_db.Include<Film>();

				List<GenreDTO>? genres = await _db.GetAsync<Genre, GenreDTO>();

				return Results.Ok(genres);
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
				
				var exists = await _db.AnyAsync<Genre>(d => d.Id.Equals(id));
				if (!exists) return Results.NotFound("Genren hittades inte.");

				var genre = await _db.SingleAsync<Genre, GenreDTO>(c => c.Id.Equals(id));

				return Results.Ok(genre);
			}
			catch
			{
			}
			return Results.NotFound();
		}

		//// POST: CREATE FILM
		[HttpPost]
		public async Task<IResult> Post([FromBody] CreateGenreDTO dto)
		{
			try
			{
				if (dto == null) return Results.BadRequest();

				var genre = await _db.AddAsync<Genre, CreateGenreDTO>(dto);

				var success = await _db.SaveChangesAsync();

				if (!success) return Results.BadRequest();

				return Results.Created(_db.GetURI<Genre>(genre), genre);
			}
			catch
			{
			}

			return Results.BadRequest();
		}

		//// PUT: EDIT FILM
		[HttpPut("{id}")]
		public async Task<IResult> Put(int id, [FromBody] GenreEditDTO dto)
		{
			try
			{
				if (dto == null) return Results.BadRequest("Ingen genre hittades");
				if (!id.Equals(dto.Id)) return Results.BadRequest("Ingen genre med detta ID hittades");

				_db.Update<Genre, GenreEditDTO>(dto.Id, dto);


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
				_db.IncludeRef<FilmGenre>();
				_db.Include<Film>();
				_db.Include<Genre>();

                await _db.DeleteGenreFilmAsync(id);
                var success = await _db.DeleteAsync<Genre>(id);

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
}
