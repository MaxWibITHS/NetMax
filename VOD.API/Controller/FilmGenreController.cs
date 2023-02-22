using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;
using VOD.Database.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VOD.API.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class FilmGenreController : ControllerBase
	{
		private readonly IDbService _db;
		public FilmGenreController(IDbService db)
		{
			_db = db;
		}

		[HttpPost]
		public async Task<IResult> Post([FromBody] FilmGenreDTO dto)
		{
			try
			{
                if (dto == null) return Results.BadRequest();
                var filmGenre = _db.AddRefAsync<FilmGenre, FilmGenreDTO>(dto);

				var success = await _db.SaveChangesAsync();

				if (!success) return Results.BadRequest();

				return Results.Ok();

            }
			catch { }

			return Results.BadRequest("Kunde ej lägga till.");

		}

        [HttpDelete("{filmId:int}/{genreId:int}", Name = "Delete")]
        public async Task<IResult> Delete(int filmId, int genreId)
        {
            try
			{
                var success = _db.DeleteRef<FilmGenre, FilmGenreDTO>(new FilmGenreDTO { FilmId = filmId, GenreId = genreId });
                if (!success ) return Results.NotFound("Kund ej hitta det du ville ta bort.");	

				var result = await _db.SaveChangesAsync();
				if (!result) return Results.BadRequest("Kunde ej ta bort");

				return Results.NoContent();
			}
			catch
			{
				return Results.BadRequest("Kunde ej ta bort");
			}
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                List<FilmGenreDTO>? filmgenres = await _db.GetAsyncRef<FilmGenre, FilmGenreDTO>();

                return Results.Ok(filmgenres);
            }
            catch
            {
            }

            return Results.NotFound();

        }

        [HttpPut]
        public async Task<IResult> Put(int id, FilmGenreDTO dto)
        {
            try
            {
                if (dto == null) return Results.BadRequest("Ingen genre hittades");
                if (!id.Equals(dto)) return Results.BadRequest("Ingen genre med detta ID hittades");

                _db.UpdateRef<FilmGenre, FilmGenreDTO>(id, dto);


                var success = await _db.SaveChangesAsync();

                if (!success) return Results.BadRequest();

                return Results.NoContent();
            }
            catch
            {
            }

            return Results.BadRequest("Kunde ej uppdatera");

        }
    }
}



