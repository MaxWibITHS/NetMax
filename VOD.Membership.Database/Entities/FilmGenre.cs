using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Database.Entities
{
    public class FilmGenre : IEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int FilmId { get; set; }     
    }
}
