using System;
using System.Collections.Generic;

namespace EFDBFirstDemo.DataAccess
{
    public partial class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public virtual Genre Genre { get; set; }
    }
}
