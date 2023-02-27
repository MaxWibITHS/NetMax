
namespace VOD.Common.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly MembershipHttpClient _http;

        public MembershipService(MembershipHttpClient http)
        {
            _http = http;
        }


        public async Task<List<FilmDTO>> GetFilmsAsync(bool freeOnly)
        {
            try
            {
                using HttpResponseMessage response = await _http.Client.GetAsync($"film?freeOnly={freeOnly}");

                response.EnsureSuccessStatusCode();

                var result = JsonSerializer.Deserialize<List<FilmDTO>>(await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result ?? new List<FilmDTO>();
            }
            catch
            {
                throw;
            }

        }


        public async Task<FilmDTO> GetFilmAsync(int? id)
        {
            try
            {
                if (id == null) throw new ArgumentNullException("id");
                using HttpResponseMessage response = await _http.Client.GetAsync($"film/{id}");

                response.EnsureSuccessStatusCode();

                var result = JsonSerializer.Deserialize<FilmDTO>(await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result ?? new();
            }
            catch
            {
                throw;
            }

        }

        public async Task<GenreDTO> GetGenreAsync(int id)
        {
            try
            {
                using HttpResponseMessage response = await _http.Client.GetAsync($"genre/{id}");

                response.EnsureSuccessStatusCode();

                var result = JsonSerializer.Deserialize<GenreDTO>(await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result ?? new();
            }
            catch
            {
                throw;
            }

        }

        //
        public async Task<List<TDto>> GetAsync<TDto>(string uri)
        {
            try
            {
                using HttpResponseMessage response = await _http.Client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var result = JsonSerializer.Deserialize<List<TDto>>(await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result ?? new List<TDto>();
            }
            catch (Exception ex) { throw; }
        }

    }
}

