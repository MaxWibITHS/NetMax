
namespace VOD.Database.Services
{
	public class DbService : IDbService
	{
		private readonly VODContext _db;
		private readonly IMapper _mapper;

		public DbService(VODContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		public async Task<List<TDto>> GetAsync<TEntity, TDto>()
		where TEntity : class, IEntity
		where TDto : class
		{
			var entities = await _db.Set<TEntity>().ToListAsync();
			var dtos = _mapper.Map<List<TDto>>(entities);
			return dtos;
		}

		public async Task<List<TDto>> GetAsync<TEntity, TDto>(
		 Expression<Func<TEntity, bool>> expression)
		where TEntity : class, IEntity
		where TDto : class
		{
			var entities = await _db.Set<TEntity>().Where(expression).ToListAsync();
			return _mapper.Map<List<TDto>>(entities);
		}

		private async Task<TEntity?> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
			where TEntity : class, IEntity => await _db.Set<TEntity>().SingleOrDefaultAsync(expression);

		public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
			where TEntity : class, IEntity
			where TDto : class
		{
			var entity = await SingleAsync(expression);
			return _mapper.Map<TDto>(entity);
		}

		public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
			where TEntity : class, IEntity
			where TDto : class
		{
			var entity = _mapper.Map<TEntity>(dto);
			await _db.Set<TEntity>().AddAsync(entity);
			return entity;
		}

		public async Task<bool> SaveChangesAsync() => await _db.SaveChangesAsync() >= 0;


		public string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity => $"/{typeof(TEntity).Name.ToLower()}s/{entity.Id}";

		public void Update<TEntity, TDto>(int id, TDto dto)
			where TEntity : class, IEntity
			where TDto : class
		{
			var entity = _mapper.Map<TEntity>(dto);
			entity.Id = id;
			_db.Set<TEntity>().Update(entity);
		}

		public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
			where TEntity : class, IEntity => await _db.Set<TEntity>().AnyAsync(expression);

		public async Task<bool> DeleteAsync<TEntity>(int id)
			where TEntity : class, IEntity
		{
			try
			{
				var entity = await SingleAsync<TEntity>(e => e.Id.Equals(id));
				if (entity is null) return false;
				_db.Remove(entity);
			}
			catch { throw; }

			return true;
		}

		public void Include<TEntity>() where TEntity : class, IEntity
		{
			var propertyNames =
			_db.Model.FindEntityType(typeof(TEntity))?.GetNavigations().Select(e =>
			e.Name);
			foreach (var name in propertyNames)
				_db.Set<TEntity>().Include(name).Load();

		}
		public void IncludeRef<TReferenceEntity>() where TReferenceEntity : class, IReferenceEntity
		{
			var propertyNames = _db.Model.FindEntityType(typeof(TReferenceEntity))?
				.GetNavigations().Select(e => e.Name); 
			
			if (propertyNames is null) return; 
			
			foreach (var name in propertyNames)
				_db.Set<TReferenceEntity>().Include(name).Load();
		}

		//

		public async Task<bool> DeleteSimilarFilmsAsync(int id)
		{
			try
			{
				var similars = await _db.Set<SimilarFilm>()
					.Where(SimilarFilm => (SimilarFilm.FilmId == id) || (SimilarFilm.SimilarFilmId == id)).ToListAsync();

                foreach (var similar in similars)
					_db.Remove(similar);

				return await _db.SaveChangesAsync() >= 0;
            }
			catch
			{
				throw;
			}
		}

        public async Task<bool> DeleteFilmGenreAsync(int Id)
		{
			try
			{
                var genres = await _db.Set<FilmGenre>()
                     .Where(fg => fg.FilmId == Id)
                     .ToListAsync();

                foreach (var genre in genres)
                    _db.Remove(genre);
                return await _db.SaveChangesAsync() >= 0;
            }
			catch
			{
				throw;
			}
		}

        public async Task<bool> DeleteGenreFilmAsync(int Id)
        {
            try
            {
                var films = await _db.Set<FilmGenre>()
                     .Where(fg => fg.GenreId == Id)
                     .ToListAsync();

                foreach (var film in films)
                    _db.Remove(film);
                return await _db.SaveChangesAsync() >= 0;
            }
            catch
            {
                throw;
            }
        }




        public async Task<TReferenceEntity> AddRefAsync<TReferenceEntity, TDto>(TDto dto) where TReferenceEntity : class,
            IReferenceEntity where TDto : class
		{
            var entity = _mapper.Map<TReferenceEntity>(dto); await _db.Set<TReferenceEntity>().AddAsync(entity);
			
			return entity;
        }

        public bool DeleteRef<TReferenceEntity, TDto>(TDto dto) where TReferenceEntity : class, IReferenceEntity where TDto : class
		{
			try
			{
                var entity = _mapper.Map<TReferenceEntity>(dto); if (entity is null) return false;
				
				_db.Remove(entity);
            }
			catch
			{
				throw;
			}
            return true;
        }

        public async Task<List<TDto>> GetAsyncRef<TReferenceEntity, TDto>()
        where TReferenceEntity : class, IReferenceEntity
        where TDto : class
        {
            var entities = await _db.Set<TReferenceEntity>().ToListAsync();
            var dtos = _mapper.Map<List<TDto>>(entities);
            return dtos;
        }

        public void UpdateRef<TReferenceEntity, TDto>(int id, TDto dto)
          where TReferenceEntity : class, IReferenceEntity
          where TDto : class
        {
            var entity = _mapper.Map<TDto>(dto);
            
            _db.Set<TDto>().Update(entity);
        }


    }
}


