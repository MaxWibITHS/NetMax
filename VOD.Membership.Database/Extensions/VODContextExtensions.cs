using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using VOD.Database.Services;
using VOD.Common.DTOs;
using static System.Net.WebRequestMethods;

namespace VOD.Database.Extensions
{
	public static class VODContextExtensions
	{
		public static async Task SeedMembershipData(this IDbService service)
		{

			try

			{
				//#region Add Director

				//await service.AddAsync<Director, DirectorDTO>(new DirectorDTO
				//{

				//	Name = "Johannes",

				//});

				//await service.AddAsync<Director, DirectorDTO>(new DirectorDTO
				//{

				//	Name = "Nina",

				//});
				//await service.SaveChangesAsync();
				//#endregion


				//#region Add Film

				//await service.AddAsync<Film, FilmDTO>(new FilmDTO
				//{

				//	Title = "Avatar 2",
				//	Released = new DateTime(2022, 01, 01),
				//	DirectorId = 1,
				//	Free = true,
				//	Description = "Hej hej hej!",
				//	Duration = new TimeSpan(2, 45, 20),
				//	FilmUrl = "https://www.youtube.com/watch?v=d9MyW72ELq0",
				//	FilmThumbnail = "...."

				//});

				//await service.AddAsync<Film, FilmDTO>(new FilmDTO
				//{

				//	Title = "Lion King",
				//	Released = new DateTime(1994, 01, 01),
				//	DirectorId = 2,
				//	Free = false,
				//	Description = "Hejdåhejdåhejdå!",
				//	Duration = new TimeSpan(1, 5, 2),
				//	FilmUrl = "https://www.youtube.com/watch?v=lFzVJEksoDY",
				//	FilmThumbnail = "....",

				//});

				//await service.SaveChangesAsync();
				//#endregion

				//#region Add Genre

				//await service.AddAsync<Genre, GenreDTO>(new GenreDTO
				//{
				//	Name = "Drama",
				//});

				//await service.AddAsync<Genre, GenreDTO>(new GenreDTO
				//{
				//	Name = "Action",
				//});

				//await service.AddAsync<Genre, GenreDTO>(new GenreDTO
				//{
				//	Name = "Comedy",
				//});

				//await service.SaveChangesAsync();
				//#endregion

			}

			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
