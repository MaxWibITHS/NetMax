using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOs;
using static System.Collections.Specialized.BitVector32;

namespace VOD.Database.Entities
{
    public class Film : IEntity
    {
        public Film()
        {
            SimilarFilms = new HashSet<SimilarFilm>();
            Genres = new HashSet<Genre>();
		}
        public int Id { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "The title name is too long")]
        [MinLength(1, ErrorMessage = "The title name is too short")]
        public string Title { get; set; } = null!;
        public string Released { get; set; }

        public int? DirectorId { get; set; } = null!;
		[Required]
		public bool Free { get; set; }
        [StringLength(200, ErrorMessage = "The description is too long")]
        [MinLength(1, ErrorMessage = "The description is too short")]
        public string Description { get; set; } = null!;
        
        public string Duration { get; set; }
		[Required]
		[Url]
		public string FilmUrl { get; set; }
        public string FilmThumbnail { get; set; }

        public virtual ICollection<Genre>? Genres { get; set; } = null!;

        public virtual ICollection<SimilarFilm> SimilarFilms { get; set; } = null!;

        public virtual Director Director { get; set; }

	}
}















