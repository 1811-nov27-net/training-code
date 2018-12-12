using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Models
{
    public class Movie
    {
        // implicitly Required since it's not a nullable type
        public int Id { get; set; }

        // Data Annotations are used by ASP.NET for client-side AND server-side validation
        [Required]
        [StringLength(50)] // max 50 chars
        public string Title { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReleaseDate { get; set; }

        public List<string> Cast { get; set; } = new List<string>();
    }
}
