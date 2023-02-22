using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VOD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimilarFilmController : ControllerBase
    {
        private readonly IDbService _db;
        public SimilarFilmController(IDbService db)
        {
            _db = db;
        }


        [HttpPost]
        public async Task<IResult> Post([FromBody] SimilarFilmDTO dto)
        {
            try
            {
                if (dto == null) return Results.BadRequest();
                var silmilarFilm = _db.AddRefAsync<SimilarFilm, SimilarFilmDTO>(dto);

                var success = await _db.SaveChangesAsync();

                if (!success) return Results.BadRequest();

                return Results.Ok();

            }

            catch { }

            return Results.BadRequest("Kunde ej lägga till.");

        }

        [HttpDelete("{filmId:int}/{similarFilmId:int}", Name = "Deletes")]
        public async Task<IResult> Delete(int filmId, int similarfilmId)
        {
            try
            {
                var success = _db.DeleteRef<SimilarFilm, SimilarFilmDTO>(new SimilarFilmDTO { FilmId = filmId, SimilarFilmId = similarfilmId });
                if (!success) return Results.NotFound("Kund ej hitta det du ville ta bort.");

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
                List<SimilarFilmDTO>? similarfilms = await _db.GetAsyncRef<SimilarFilm, SimilarFilmDTO>();

                return Results.Ok(similarfilms);
            }
            catch
            {
            }

            return Results.NotFound();

        }

        [HttpPut]
        public async Task<IResult> Put(int id, SimilarFilmDTO dto)
        {
            try
            {
                if (dto == null) return Results.BadRequest("Ingen genre hittades");
                if (!id.Equals(dto)) return Results.BadRequest("Ingen genre med detta ID hittades");

                _db.UpdateRef<SimilarFilm, SimilarFilmDTO>(id, dto);


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
