namespace VOD.Common.Services
{
    public interface IMembershipService
    {
        Task<List<TDto>> GetAsync<TDto>(string uri);
        Task<FilmDTO> GetFilmAsync(int? id);
        Task<List<FilmDTO>> GetFilmsAsync(bool freeOnly);
        Task<GenreDTO> GetGenreAsync(int id);
    }
}