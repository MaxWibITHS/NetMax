using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Database.Entities
{
    public class Director : IEntity
    {
        //public Director()
        //{
        //    Films = new HashSet<Film>();
        //}
        public int Id { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "The name is too long")]
        [MinLength(1, ErrorMessage = "The name is too short")]
        public string Name { get; set; }

		public virtual ICollection<Film> Film { get; set; } = null!;

	}
}




