using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.DTOs
{
    public class GenreDTO
    {
		public GenreDTO()
		{
			Films = new List<FilmDTO>();
		}
		public int Id { get; set; }
		
		public string Name { get; set; }
		public List <FilmDTO>? Films { get; set; }
	
	}
	public class CreateGenreDTO
	{
		public string Name { get; set; }
	}

	public class GenreEditDTO : CreateDirectorDTO
	{
		public int Id { get; set; }
	}
}










