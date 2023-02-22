using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.DTOs
{
	public class FilmDTO
	{
		public FilmDTO()
		{
			SimilarFilms = new List<string>();
			Genres = new List<string>();
		}
		public int Id { get; set; }

		public string Title { get; set; }
		public string Released { get; set; }

		public int? DirectorId { get; set; }
		public bool Free { get; set; }

		public string Description { get; set; }
		public string Duration { get; set; }

		public string FilmUrl { get; set; }
		public string FilmThumbnail { get; set; }

		public List<string>? Genres { get; set; }

		public List<string>? SimilarFilms { get; set; }
		
		public string? Director { get; set; }

	}

	public class CreateFilmDTO
	{
		public string Title { get; set; }
		public string Released { get; set; }

		public int DirectorId { get; set; }
		public bool Free { get; set; }

		public string Description { get; set; }
		public string Duration { get; set; }

		public string FilmUrl { get; set; }
		public string FilmThumbnail { get; set; }
	}

	public class FilmEditDTO : CreateFilmDTO
	{
		public int Id { get; set; }
	}
}






















