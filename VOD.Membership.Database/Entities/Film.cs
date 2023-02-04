using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace VOD.Database.Entities
{
    public class Film : IEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "The title name is too long")]
        [MinLength(1, ErrorMessage = "The title name is too short")]
        public string Title { get; set; }
        public DateTime Released { get; set; }
        [Required]
        public int DirectorId { get; set; }
        [StringLength(200, ErrorMessage = "The description is too long")]
        [MinLength(1, ErrorMessage = "The description is too short")]
        public string Description { get; set; }
        [Required]
        [Url]
        public string FilmUrl { get; set; }

        public virtual Director Director { get; set; }

        public virtual ICollection<SimilarFilm> SimilarFilm { get; set; }
        public virtual ICollection<FilmGenre> FilmGenre { get; set; }
    }
}


