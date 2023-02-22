using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Database.Entities
{
    public class FilmGenre : IReferenceEntity
    {
        [Required]
        public int FilmId { get; set; } 
       
        public int? GenreId { get; set; }

		public virtual Film Film { get; set; } = null!;
		public virtual Genre Genre { get; set; } = null!;
	}
}


