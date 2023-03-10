using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.DTOs
{
    public class DirectorDTO
    {
		public int Id { get; set; }
	
		public string Name { get; set; }

		public List<FilmDTO> Film { get; set; }

	}
	public class CreateDirectorDTO
	{
		public string Name { get; set; }
	}

	public class DirectorEditDTO : CreateDirectorDTO
	{
		public int Id { get; set; }
	}
}





