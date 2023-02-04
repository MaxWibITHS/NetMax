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
        public int Id { get; set; }
        
        public string Title { get; set; }
        public DateTime Released { get; set; }
        
        public int DirectorId { get; set; }
        
        public string Description { get; set; }
        
        public string FilmUrl { get; set; }

        public virtual DirectorDTO Director { get; set; }

        public List<SimilarFilmDTO> SimilarFilm { get; set; } = new();
        public List<FilmGenreDTO> FilmGenre { get; set; } = new();

    }
}







