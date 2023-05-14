using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H645NF_HFT_2022231.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Runtime { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public int Budget { get; set; }

        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        [NotMapped]
        public virtual Genre Genre { get; set; }
        [NotMapped]
        public virtual ICollection<Rent> Rents { get; set; }

        public Movie()
        {
            
        }
        public Movie(int movieid, string title, int runtime, int year, string country, int budget, int genreid)
        {
            MovieId = movieid;
            Title = title;
            Runtime = runtime;
            Year = year;
            Country = country;
            Budget = budget;
            GenreId = genreid;
        }
    }
}
