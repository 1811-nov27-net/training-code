using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCDemo.DataAccess
{
    public class Movie
    {
        // here we're using DataAnnotations to configure the generated SQL DB
        // but the fluent API in OnModelCreating is more flexible

        // mark primary key with [Key]
        [Key] // but it does infer that anything named "MovieId" or "Id" or "ID" etc will be the primary key
        // if not otherwise configured.
        public int Id { get; set; }

        [Required]
        [StringLength(50)] // max 50 chars
        public string Title { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReleaseDate { get; set; }

        // navigation property
        public virtual ICollection<MovieCastMemberJunction> CastMemberJunctions { get; set; }
    }
}