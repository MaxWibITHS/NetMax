using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Database.Services
{
	public interface IDbService
	{
		Task<List<TDto>> GetAsync<TEntity, TDto>()
		   where TEntity : class, IEntity
		   where TDto : class;


		Task<List<TDto>> GetAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
		where TEntity : class, IEntity
		where TDto : class;

		Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
			where TEntity : class, IEntity
			where TDto : class;


		Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
			where TEntity : class, IEntity
			where TDto : class;


		void Update<TEntity, TDto>(int id, TDto dto)
			where TEntity : class, IEntity
			where TDto : class;

		Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
			where TEntity : class, IEntity;

		Task<bool> DeleteAsync<TEntity>(int id)
			where TEntity : class, IEntity;

		Task<bool> SaveChangesAsync();

		public void Include<TEntity>() where TEntity : class, IEntity;

		public void IncludeRef<TReferenceEntity>() where TReferenceEntity : class, IReferenceEntity;

		string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity;

		//

        Task<bool> DeleteFilmGenreAsync(int Id);
        Task<bool> DeleteSimilarFilmsAsync(int id);


        bool DeleteRef<TReferenceEntity, TDto>(TDto dto)
            where TReferenceEntity : class, IReferenceEntity
            where TDto : class;
        Task<TReferenceEntity> AddRefAsync<TReferenceEntity, TDto>(TDto dto)
            where TReferenceEntity : class, IReferenceEntity
            where TDto : class;
        Task<List<TDto>> GetAsyncRef<TReferenceEntity, TDto>()
            where TReferenceEntity : class, IReferenceEntity
            where TDto : class;
        void UpdateRef<TReferenceEntity, TDto>(int id, TDto dto)
            where TReferenceEntity : class, IReferenceEntity
            where TDto : class;
        Task<bool> DeleteGenreFilmAsync(int Id);
    }
}


