using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Database.Entities
{
    public class SimilarFilm : IEntity
    {
        public int Id { get; set; }
        public int ParentFilmId { get; set; }

        public virtual ICollection<Film> ListSimilarFilms { get; set; }

    }
}


