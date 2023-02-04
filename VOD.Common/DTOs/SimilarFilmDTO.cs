using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.DTOs
{
    public class SimilarFilmDTO
    {
        public int Id { get; set; }
        public int ParentFilmId { get; set; }

        public List <FilmDTO> ListSimilarFilms { get; set; }

    }
}



