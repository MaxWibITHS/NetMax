
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VOD.API.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class DirectorController : ControllerBase
	{
		private readonly IDbService _db;
		public DirectorController(IDbService db)
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

				List<DirectorDTO>? directors = await _db.GetAsync<Director, DirectorDTO>();

				return Results.Ok(directors);
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

				var exists = await _db.AnyAsync<Director>(d => d.Id.Equals(id));
				if (!exists) return Results.NotFound("Director hittades inte.");

				var director = await _db.SingleAsync<Director, DirectorDTO>(c => c.Id.Equals(id));

				return Results.Ok(director);
			}
			catch
			{
			}
			return Results.NotFound();
		}

		//// POST: CREATE DIRECTOR
		[HttpPost]
		public async Task<IResult> Post([FromBody] CreateDirectorDTO dto)
		{
			try
			{
				
				if (dto == null) return Results.BadRequest();

				var director = await _db.AddAsync<Director, CreateDirectorDTO>(dto);

				var success = await _db.SaveChangesAsync();

				if (!success) return Results.BadRequest();

				return Results.Created(_db.GetURI<Director>(director), director);
			}
			catch
			{
			}

			return Results.BadRequest();
		}

		//// PUT: EDIT FILM
		[HttpPut("{id}")]
		public async Task<IResult> Put(int id, [FromBody] DirectorEditDTO dto)
		{
			try
			{
				if (dto == null) return Results.BadRequest("Ingen director hittades");
				if (!id.Equals(dto.Id)) return Results.BadRequest("Ingen director med detta ID hittades");

				_db.Update<Director, DirectorEditDTO>(dto.Id, dto);


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
				_db.Include<Film>();

				var success = await _db.DeleteAsync<Director>(id);
				

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